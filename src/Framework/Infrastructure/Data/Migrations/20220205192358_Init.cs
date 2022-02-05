using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.CreateTable(
                   name: "asp_net_roles",
                   columns: table => new
                   {
                       id = table.Column<int>(type: "integer", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                       description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                       created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                       name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                       normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                       concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                   },
                   constraints: table =>
                   {
                       table.PrimaryKey("pk_asp_net_roles", x => x.id);
                   });

                migrationBuilder.CreateTable(
                    name: "file_model",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                        file_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                        extension = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                        description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                        discriminator = table.Column<string>(type: "text", nullable: false),
                        data = table.Column<byte[]>(type: "bytea", nullable: true),
                        file_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_file_model", x => x.id);
                    });

                migrationBuilder.CreateTable(
                    name: "tenant",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_tenant", x => x.id);
                    });

                migrationBuilder.CreateTable(
                    name: "ticket_type",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_ticket_type", x => x.id);
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_role_claims",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        role_id = table.Column<int>(type: "integer", nullable: false),
                        claim_type = table.Column<string>(type: "text", nullable: true),
                        claim_value = table.Column<string>(type: "text", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                        table.ForeignKey(
                            name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                            column: x => x.role_id,
                            principalTable: "asp_net_roles",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "team",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                        description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                        parent_id = table.Column<int>(type: "integer", nullable: true),
                        tenant_id = table.Column<int>(type: "integer", nullable: true),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_team", x => x.id);
                        table.CheckConstraint("CK_team_chk_tenant", "(( parent_id IS NULL AND tenant_id IS NOT NULL) OR (tenant_id IS NULL AND parent_id IS NOT NULL))");
                        table.ForeignKey(
                            name: "fk_team_team_parent_id",
                            column: x => x.parent_id,
                            principalTable: "team",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_team_tenant_tenant_id",
                            column: x => x.tenant_id,
                            principalTable: "tenant",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_users",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        firstname = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                        lastname = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                        birthdate = table.Column<DateTime>(type: "date", nullable: false),
                        gender = table.Column<byte>(type: "smallint", nullable: false),
                        is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                        last_login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                        refresh_token = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                        refresh_token_expiration_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                        team_id = table.Column<int>(type: "integer", nullable: false),
                        profile_picture_id = table.Column<int>(type: "integer", nullable: true),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                        user_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                        normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                        email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                        normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                        email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                        password_hash = table.Column<string>(type: "text", nullable: true),
                        security_stamp = table.Column<string>(type: "text", nullable: true),
                        concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                        phone_number = table.Column<string>(type: "text", nullable: true),
                        phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                        two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                        lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                        lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                        access_failed_count = table.Column<int>(type: "integer", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_users", x => x.id);
                        table.ForeignKey(
                            name: "fk_asp_net_users_file_model_profile_picture_id",
                            column: x => x.profile_picture_id,
                            principalTable: "file_model",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_asp_net_users_team_team_id",
                            column: x => x.team_id,
                            principalTable: "team",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_user_claims",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        user_id = table.Column<int>(type: "integer", nullable: false),
                        claim_type = table.Column<string>(type: "text", nullable: true),
                        claim_value = table.Column<string>(type: "text", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                        table.ForeignKey(
                            name: "fk_asp_net_user_claims_asp_net_users_user_id",
                            column: x => x.user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_user_logins",
                    columns: table => new
                    {
                        login_provider = table.Column<string>(type: "text", nullable: false),
                        provider_key = table.Column<string>(type: "text", nullable: false),
                        provider_display_name = table.Column<string>(type: "text", nullable: true),
                        user_id = table.Column<int>(type: "integer", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                        table.ForeignKey(
                            name: "fk_asp_net_user_logins_asp_net_users_user_id",
                            column: x => x.user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_user_roles",
                    columns: table => new
                    {
                        user_id = table.Column<int>(type: "integer", nullable: false),
                        role_id = table.Column<int>(type: "integer", nullable: false),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                        table.ForeignKey(
                            name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                            column: x => x.role_id,
                            principalTable: "asp_net_roles",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_asp_net_user_roles_asp_net_users_user_id",
                            column: x => x.user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "asp_net_user_tokens",
                    columns: table => new
                    {
                        user_id = table.Column<int>(type: "integer", nullable: false),
                        login_provider = table.Column<string>(type: "text", nullable: false),
                        name = table.Column<string>(type: "text", nullable: false),
                        value = table.Column<string>(type: "text", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                        table.ForeignKey(
                            name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                            column: x => x.user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "emails_log",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        to_email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                        subject = table.Column<string>(type: "text", nullable: false),
                        body = table.Column<string>(type: "text", nullable: false),
                        to_user_id = table.Column<int>(type: "integer", nullable: true),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_emails_log", x => x.id);
                        table.CheckConstraint("CK_emails_log_chk_emails_log", "(( to_user_id IS NULL AND to_email IS NOT NULL) OR (to_email IS NULL AND to_user_id IS NOT NULL))");
                        table.ForeignKey(
                            name: "fk_emails_log_asp_net_users_to_user_id",
                            column: x => x.to_user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "ticket",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                        description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                        ticket_status = table.Column<byte>(type: "smallint", nullable: false),
                        ticket_type_id = table.Column<int>(type: "integer", nullable: false),
                        team_id = table.Column<int>(type: "integer", nullable: false),
                        issuer_user_id = table.Column<int>(type: "integer", nullable: false),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_ticket", x => x.id);
                        table.ForeignKey(
                            name: "fk_ticket_asp_net_users_issuer_user_id",
                            column: x => x.issuer_user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_ticket_team_team_id",
                            column: x => x.team_id,
                            principalTable: "team",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_ticket_ticket_type_ticket_type_id",
                            column: x => x.ticket_type_id,
                            principalTable: "ticket_type",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "emails_log_file_model",
                    columns: table => new
                    {
                        emails_log_id = table.Column<int>(type: "integer", nullable: false),
                        file_model_id = table.Column<int>(type: "integer", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_emails_log_file_model", x => new { x.emails_log_id, x.file_model_id });
                        table.ForeignKey(
                            name: "fk_emails_log_file_model_emails_log_emails_log_id",
                            column: x => x.emails_log_id,
                            principalTable: "emails_log",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_emails_log_file_model_file_model_file_model_id",
                            column: x => x.file_model_id,
                            principalTable: "file_model",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateTable(
                    name: "ticket_process",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        title = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                        description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                        ticket_id = table.Column<int>(type: "integer", nullable: false),
                        team_id = table.Column<int>(type: "integer", nullable: false),
                        assigned_user_id = table.Column<int>(type: "integer", nullable: true),
                        parent_ticket_process_id = table.Column<int>(type: "integer", nullable: true),
                        created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("pk_ticket_process", x => x.id);
                        table.ForeignKey(
                            name: "fk_ticket_process_asp_net_users_assigned_user_id",
                            column: x => x.assigned_user_id,
                            principalTable: "asp_net_users",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_ticket_process_team_team_id",
                            column: x => x.team_id,
                            principalTable: "team",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_ticket_process_ticket_process_parent_ticket_process_id",
                            column: x => x.parent_ticket_process_id,
                            principalTable: "ticket_process",
                            principalColumn: "id");
                        table.ForeignKey(
                            name: "fk_ticket_process_ticket_ticket_id",
                            column: x => x.ticket_id,
                            principalTable: "ticket",
                            principalColumn: "id");
                    });

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_role_claims_role_id",
                    table: "asp_net_role_claims",
                    column: "role_id");

                migrationBuilder.CreateIndex(
                    name: "role_name_index",
                    table: "asp_net_roles",
                    column: "normalized_name",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_user_claims_user_id",
                    table: "asp_net_user_claims",
                    column: "user_id");

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_user_logins_user_id",
                    table: "asp_net_user_logins",
                    column: "user_id");

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_user_roles_role_id",
                    table: "asp_net_user_roles",
                    column: "role_id");

                migrationBuilder.CreateIndex(
                    name: "email_index",
                    table: "asp_net_users",
                    column: "normalized_email");

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_users_profile_picture_id",
                    table: "asp_net_users",
                    column: "profile_picture_id",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "ix_asp_net_users_team_id",
                    table: "asp_net_users",
                    column: "team_id");

                migrationBuilder.CreateIndex(
                    name: "user_name_index",
                    table: "asp_net_users",
                    column: "normalized_user_name",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "ix_emails_log_to_user_id",
                    table: "emails_log",
                    column: "to_user_id");

                migrationBuilder.CreateIndex(
                    name: "ix_emails_log_file_model_file_model_id",
                    table: "emails_log_file_model",
                    column: "file_model_id",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "ix_team_parent_id",
                    table: "team",
                    column: "parent_id");

                migrationBuilder.CreateIndex(
                    name: "ix_team_tenant_id",
                    table: "team",
                    column: "tenant_id");

                migrationBuilder.CreateIndex(
                    name: "ix_tenant_name",
                    table: "tenant",
                    column: "name",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_issuer_user_id",
                    table: "ticket",
                    column: "issuer_user_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_team_id",
                    table: "ticket",
                    column: "team_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_ticket_type_id",
                    table: "ticket",
                    column: "ticket_type_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_process_assigned_user_id",
                    table: "ticket_process",
                    column: "assigned_user_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_process_parent_ticket_process_id",
                    table: "ticket_process",
                    column: "parent_ticket_process_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_process_team_id",
                    table: "ticket_process",
                    column: "team_id");

                migrationBuilder.CreateIndex(
                    name: "ix_ticket_process_ticket_id",
                    table: "ticket_process",
                    column: "ticket_id");
            }

            if (ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                migrationBuilder.CreateTable(
                    name: "AspNetRoles",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                        NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "FileModel",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                        FileType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                        Extension = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                        Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                        FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_FileModel", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "Tenant",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Tenant", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "TicketType",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_TicketType", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetRoleClaims",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        RoleId = table.Column<int>(type: "int", nullable: false),
                        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                        table.ForeignKey(
                            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                            column: x => x.RoleId,
                            principalTable: "AspNetRoles",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "Team",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                        Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                        ParentId = table.Column<int>(type: "int", nullable: true),
                        TenantId = table.Column<int>(type: "int", nullable: true),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Team", x => x.Id);
                        table.CheckConstraint("CK_Team_CHK_Tenant", "((ParentId IS NULL AND TenantId IS NOT NULL) OR (TenantId IS NULL AND ParentId IS NOT NULL))");
                        table.ForeignKey(
                            name: "FK_Team_Team_ParentId",
                            column: x => x.ParentId,
                            principalTable: "Team",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_Team_Tenant_TenantId",
                            column: x => x.TenantId,
                            principalTable: "Tenant",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetUsers",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Firstname = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                        Lastname = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                        Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                        Gender = table.Column<byte>(type: "tinyint", nullable: false),
                        IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                        LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                        RefreshToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                        RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                        TeamId = table.Column<int>(type: "int", nullable: false),
                        ProfilePictureId = table.Column<int>(type: "int", nullable: true),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UserName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                        Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                        table.ForeignKey(
                            name: "FK_AspNetUsers_FileModel_ProfilePictureId",
                            column: x => x.ProfilePictureId,
                            principalTable: "FileModel",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_AspNetUsers_Team_TeamId",
                            column: x => x.TeamId,
                            principalTable: "Team",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetUserClaims",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        UserId = table.Column<int>(type: "int", nullable: false),
                        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                        table.ForeignKey(
                            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetUserLogins",
                    columns: table => new
                    {
                        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        UserId = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                        table.ForeignKey(
                            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetUserRoles",
                    columns: table => new
                    {
                        UserId = table.Column<int>(type: "int", nullable: false),
                        RoleId = table.Column<int>(type: "int", nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                        table.ForeignKey(
                            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                            column: x => x.RoleId,
                            principalTable: "AspNetRoles",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "AspNetUserTokens",
                    columns: table => new
                    {
                        UserId = table.Column<int>(type: "int", nullable: false),
                        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                        table.ForeignKey(
                            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "EmailsLog",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        ToEmail = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                        Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        ToUserId = table.Column<int>(type: "int", nullable: true),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_EmailsLog", x => x.Id);
                        table.CheckConstraint("CK_EmailsLog_CHK_EmailsLog", "((ToUserId IS NULL AND ToEmail IS NOT NULL) OR (ToEmail IS NULL AND ToUserId IS NOT NULL))");
                        table.ForeignKey(
                            name: "FK_EmailsLog_AspNetUsers_ToUserId",
                            column: x => x.ToUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "Ticket",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                        Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                        TicketStatus = table.Column<byte>(type: "tinyint", nullable: false),
                        TicketTypeId = table.Column<int>(type: "int", nullable: false),
                        TeamId = table.Column<int>(type: "int", nullable: false),
                        IssuerUserId = table.Column<int>(type: "int", nullable: false),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Ticket", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Ticket_AspNetUsers_IssuerUserId",
                            column: x => x.IssuerUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_Ticket_Team_TeamId",
                            column: x => x.TeamId,
                            principalTable: "Team",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_Ticket_TicketType_TicketTypeId",
                            column: x => x.TicketTypeId,
                            principalTable: "TicketType",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "EmailsLogFileModel",
                    columns: table => new
                    {
                        EmailsLogId = table.Column<int>(type: "int", nullable: false),
                        FileModelId = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_EmailsLogFileModel", x => new { x.EmailsLogId, x.FileModelId });
                        table.ForeignKey(
                            name: "FK_EmailsLogFileModel_EmailsLog_EmailsLogId",
                            column: x => x.EmailsLogId,
                            principalTable: "EmailsLog",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_EmailsLogFileModel_FileModel_FileModelId",
                            column: x => x.FileModelId,
                            principalTable: "FileModel",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateTable(
                    name: "TicketProcess",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Title = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                        Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                        TicketId = table.Column<int>(type: "int", nullable: false),
                        TeamId = table.Column<int>(type: "int", nullable: false),
                        AssignedUserId = table.Column<int>(type: "int", nullable: true),
                        ParentTicketProcessId = table.Column<int>(type: "int", nullable: true),
                        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_TicketProcess", x => x.Id);
                        table.ForeignKey(
                            name: "FK_TicketProcess_AspNetUsers_AssignedUserId",
                            column: x => x.AssignedUserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_TicketProcess_Team_TeamId",
                            column: x => x.TeamId,
                            principalTable: "Team",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_TicketProcess_Ticket_TicketId",
                            column: x => x.TicketId,
                            principalTable: "Ticket",
                            principalColumn: "Id");
                        table.ForeignKey(
                            name: "FK_TicketProcess_TicketProcess_ParentTicketProcessId",
                            column: x => x.ParentTicketProcessId,
                            principalTable: "TicketProcess",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetRoleClaims_RoleId",
                    table: "AspNetRoleClaims",
                    column: "RoleId");

                migrationBuilder.CreateIndex(
                    name: "RoleNameIndex",
                    table: "AspNetRoles",
                    column: "NormalizedName",
                    unique: true,
                    filter: "[NormalizedName] IS NOT NULL");

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetUserClaims_UserId",
                    table: "AspNetUserClaims",
                    column: "UserId");

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetUserLogins_UserId",
                    table: "AspNetUserLogins",
                    column: "UserId");

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetUserRoles_RoleId",
                    table: "AspNetUserRoles",
                    column: "RoleId");

                migrationBuilder.CreateIndex(
                    name: "EmailIndex",
                    table: "AspNetUsers",
                    column: "NormalizedEmail");

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetUsers_ProfilePictureId",
                    table: "AspNetUsers",
                    column: "ProfilePictureId",
                    unique: true,
                    filter: "[ProfilePictureId] IS NOT NULL");

                migrationBuilder.CreateIndex(
                    name: "IX_AspNetUsers_TeamId",
                    table: "AspNetUsers",
                    column: "TeamId");

                migrationBuilder.CreateIndex(
                    name: "UserNameIndex",
                    table: "AspNetUsers",
                    column: "NormalizedUserName",
                    unique: true,
                    filter: "[NormalizedUserName] IS NOT NULL");

                migrationBuilder.CreateIndex(
                    name: "IX_EmailsLog_ToUserId",
                    table: "EmailsLog",
                    column: "ToUserId");

                migrationBuilder.CreateIndex(
                    name: "IX_EmailsLogFileModel_FileModelId",
                    table: "EmailsLogFileModel",
                    column: "FileModelId",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "IX_Team_ParentId",
                    table: "Team",
                    column: "ParentId");

                migrationBuilder.CreateIndex(
                    name: "IX_Team_TenantId",
                    table: "Team",
                    column: "TenantId");

                migrationBuilder.CreateIndex(
                    name: "IX_Tenant_Name",
                    table: "Tenant",
                    column: "Name",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "IX_Ticket_IssuerUserId",
                    table: "Ticket",
                    column: "IssuerUserId");

                migrationBuilder.CreateIndex(
                    name: "IX_Ticket_TeamId",
                    table: "Ticket",
                    column: "TeamId");

                migrationBuilder.CreateIndex(
                    name: "IX_Ticket_TicketTypeId",
                    table: "Ticket",
                    column: "TicketTypeId");

                migrationBuilder.CreateIndex(
                    name: "IX_TicketProcess_AssignedUserId",
                    table: "TicketProcess",
                    column: "AssignedUserId");

                migrationBuilder.CreateIndex(
                    name: "IX_TicketProcess_ParentTicketProcessId",
                    table: "TicketProcess",
                    column: "ParentTicketProcessId");

                migrationBuilder.CreateIndex(
                    name: "IX_TicketProcess_TeamId",
                    table: "TicketProcess",
                    column: "TeamId");

                migrationBuilder.CreateIndex(
                    name: "IX_TicketProcess_TicketId",
                    table: "TicketProcess",
                    column: "TicketId");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.DropTable(
                    name: "asp_net_role_claims");

                migrationBuilder.DropTable(
                    name: "asp_net_user_claims");

                migrationBuilder.DropTable(
                    name: "asp_net_user_logins");

                migrationBuilder.DropTable(
                    name: "asp_net_user_roles");

                migrationBuilder.DropTable(
                    name: "asp_net_user_tokens");

                migrationBuilder.DropTable(
                    name: "emails_log_file_model");

                migrationBuilder.DropTable(
                    name: "ticket_process");

                migrationBuilder.DropTable(
                    name: "asp_net_roles");

                migrationBuilder.DropTable(
                    name: "emails_log");

                migrationBuilder.DropTable(
                    name: "ticket");

                migrationBuilder.DropTable(
                    name: "asp_net_users");

                migrationBuilder.DropTable(
                    name: "ticket_type");

                migrationBuilder.DropTable(
                    name: "file_model");

                migrationBuilder.DropTable(
                    name: "team");

                migrationBuilder.DropTable(
                    name: "tenant");
            }

            if (ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                migrationBuilder.DropTable(
                    name: "AspNetRoleClaims");

                migrationBuilder.DropTable(
                    name: "AspNetUserClaims");

                migrationBuilder.DropTable(
                    name: "AspNetUserLogins");

                migrationBuilder.DropTable(
                    name: "AspNetUserRoles");

                migrationBuilder.DropTable(
                    name: "AspNetUserTokens");

                migrationBuilder.DropTable(
                    name: "EmailsLogFileModel");

                migrationBuilder.DropTable(
                    name: "TicketProcess");

                migrationBuilder.DropTable(
                    name: "AspNetRoles");

                migrationBuilder.DropTable(
                    name: "EmailsLog");

                migrationBuilder.DropTable(
                    name: "Ticket");

                migrationBuilder.DropTable(
                    name: "AspNetUsers");

                migrationBuilder.DropTable(
                    name: "TicketType");

                migrationBuilder.DropTable(
                    name: "FileModel");

                migrationBuilder.DropTable(
                    name: "Team");

                migrationBuilder.DropTable(
                    name: "Tenant");
            }
        }
    }
}

﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "HealthInitiative" (
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name" character varying(256) NULL,
    "TotalWeightLossGoal" decimal(4,1) NOT NULL,
    CONSTRAINT "PK_HealthInitiative" PRIMARY KEY ("Id")
);

CREATE TABLE "HealthParticipant" (
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "HealthInitiativeId" integer NOT NULL,
    "Name" character varying(256) NULL,
    "IndividualWeightLossGoal" decimal(4,1) NOT NULL,
    CONSTRAINT "PK_HealthParticipant" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_HealthParticipant_HealthInitiative_HealthInitiativeId" FOREIGN KEY ("HealthInitiativeId") REFERENCES "HealthInitiative" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ParticipantLog" (
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "ParticipantId" integer NOT NULL,
    "HealthInitiativeId" integer NOT NULL,
    "Photo" bytea NULL,
    "CurrentWeight" decimal(4,1) NOT NULL,
    CONSTRAINT "PK_ParticipantLog" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ParticipantLog_HealthInitiative_HealthInitiativeId" FOREIGN KEY ("HealthInitiativeId") REFERENCES "HealthInitiative" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ParticipantLog_HealthParticipant_ParticipantId" FOREIGN KEY ("ParticipantId") REFERENCES "HealthParticipant" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_HealthParticipant_HealthInitiativeId" ON "HealthParticipant" ("HealthInitiativeId");

CREATE INDEX "IX_ParticipantLog_HealthInitiativeId" ON "ParticipantLog" ("HealthInitiativeId");

CREATE INDEX "IX_ParticipantLog_ParticipantId" ON "ParticipantLog" ("ParticipantId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200412231159_InitialCreate', '3.1.3');

ALTER TABLE "HealthInitiative" ADD "IsCurrent" boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "HealthInitiative" ADD "StartDateTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200413013712_UpdateDbColumns', '3.1.3');

ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" TYPE timestamp without time zone;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" DROP NOT NULL;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" DROP DEFAULT;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200413030516_NullableStartDateTime', '3.1.3');

ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" TYPE timestamp without time zone;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" SET NOT NULL;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" DROP DEFAULT;

ALTER TABLE "HealthInitiative" ADD "EndDateTime" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200414014305_UpdateDateColumns', '3.1.3');

ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" TYPE timestamp without time zone;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" DROP NOT NULL;
ALTER TABLE "HealthInitiative" ALTER COLUMN "StartDateTime" DROP DEFAULT;

ALTER TABLE "HealthInitiative" ALTER COLUMN "EndDateTime" TYPE timestamp without time zone;
ALTER TABLE "HealthInitiative" ALTER COLUMN "EndDateTime" DROP NOT NULL;
ALTER TABLE "HealthInitiative" ALTER COLUMN "EndDateTime" DROP DEFAULT;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200415013652_UpdateDateColumnsToNullable', '3.1.3');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200416025655_AddRelationshipFromParticipantsToParticipantLogs', '3.1.3');

ALTER TABLE "HealthParticipant" ADD "StartingWeight" numeric NOT NULL DEFAULT 0.0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200416235128_AddStartingWeightToParticipant', '3.1.3');

ALTER TABLE "ParticipantLog" ADD "DateSubmitted" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200423011959_AddDateSubmittedToParticipantLogTable', '3.1.3');

ALTER TABLE "ParticipantLog" ADD "Points" smallint NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200424000428_AddPointsToParticipantLogs', '3.1.3');


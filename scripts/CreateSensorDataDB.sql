CREATE TABLE [dbo].[MeterReadings](
	[DeviceId] [uniqueidentifier] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[MeterReading] [decimal](18, 3) NOT NULL,
	[Consumption] [decimal](18, 3) NOT NULL,
	CONSTRAINT [PK_MeterReadings] PRIMARY KEY CLUSTERED 
	(
		[DeviceId] ASC,
		[Timestamp] DESC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
  ) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PeriodicAlertConfigurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceId] [uniqueidentifier] NOT NULL,
	[NumberOfMinutes] [int] NOT NULL,
	[ThresholdConsumption] [decimal](18, 3) NOT NULL,
	CONSTRAINT [PK_PeriodicAlertConfigurations] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
  ) ON [PRIMARY]
GO

INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('13f330e4-fc81-4ed7-815a-f20bed1095b7', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('5c15b4c8-268d-42db-9ee6-8c02aed39435', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('c8407f99-429a-4665-b76f-9558254205a9', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('eb77cf05-5b33-40ca-b110-7fd98dcd0ace', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('7b8aedb8-b6ad-468c-820b-173e74a4466a', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('729764ec-5c28-4fc5-9ed5-12778d93e04f', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('b51ee191-72d1-44d0-9b8b-5a835237b1d2', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('763a913f-ae3a-4cd1-a6ea-8b48b90c983a', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('dec84de4-bb0d-4c33-af07-55f37051277c', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('c25ecf51-d1e0-4647-8f94-f0ed33ca5e5b', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('17f7e66b-a0fe-446e-ad8f-f5072c683ad2', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('ec3bf5f0-4c84-4c67-b1cd-98e09f751812', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('9e1c590a-5bf4-433f-8967-4bf77afa4d3b', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('d16da805-5024-4cdc-abfa-282eebb7233e', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('ea6d2050-d7d7-4da6-adfd-06e66d7fe88b', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('1b504a5b-678e-4901-b211-ca311d73737e', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('9f66ca60-99ef-408d-9c1c-03fdc094e189', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('84ad09df-75f2-4e4a-8e67-cdb73f8bf587', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2c37deed-8b58-4d73-8b07-bd20da872b5a', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('59514720-0d74-40df-8829-fed49c7cf2f1', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a424050e-0277-4a06-abf9-cd65b885ae90', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('96d7d573-64cd-4026-86f3-a1ef380356cf', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('e8041904-aad5-4372-bc4b-ed0e4cf95021', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2db4cf60-8a87-4907-bd60-8734468f766f', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('746ea7c8-03d4-4c93-8194-6ee65fa72ce4', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a243f9a9-7a8a-4eb5-a1d5-39899ccb56b6', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a18ec48d-6b51-4ba3-bcdb-42afc39383ca', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('dae117d1-f79f-45cc-93e6-69056211a2d3', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('4782d8c9-8bcb-49b0-ad10-89a7ec70579e', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('3b1e9875-9542-4ae2-a36c-b8a1db420b24', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('1c620df8-93f5-414e-bec8-c249aa1ea414', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('257aebc8-b4b3-4956-933c-5ca4f33693e3', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('cb57d07f-0dcd-43a9-a574-b9475db5ff45', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a19f49fe-7f72-41fb-9e99-dcc0b83d16b9', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('52bf98ab-7107-4121-ab38-8523243de5d7', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('3dc5482c-c637-4092-8a67-935b2de1cbca', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('8ff4cb49-0797-4356-ab7e-8d1ccee3c5e4', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('d53c6e60-132b-457c-b637-4115a23405c8', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('22b335a6-26d9-492a-a937-ad771f5fa70e', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2882ac3f-2873-4855-8ae1-1502491348ae', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('4c3bfdf3-d6b3-4abb-9323-4e0f91e122a1', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('0bf4193d-4393-46cf-b19f-1f3d4d182b06', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('42e11cb0-d094-456a-a655-ee7027c81f96', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a67130e8-123f-40b6-8131-29226efb87ff', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('51e753fd-e993-4d13-bec0-9108bf5b6678', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('0001eb44-25c3-4eba-b6b1-4c08fedbfcdf', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('290be81a-aca5-4f8e-8ec8-5a65b56c69b5', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('e59f7de3-fcb3-4864-9689-67b8021507b5', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('5020f816-2d25-42da-8314-f16d3780cd74', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('7d39d562-fba4-4c3b-9064-b3d5ce0843e5', 20, 30);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('13f330e4-fc81-4ed7-815a-f20bed1095b7', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('5c15b4c8-268d-42db-9ee6-8c02aed39435', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('c8407f99-429a-4665-b76f-9558254205a9', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('eb77cf05-5b33-40ca-b110-7fd98dcd0ace', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('7b8aedb8-b6ad-468c-820b-173e74a4466a', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('729764ec-5c28-4fc5-9ed5-12778d93e04f', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('b51ee191-72d1-44d0-9b8b-5a835237b1d2', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('763a913f-ae3a-4cd1-a6ea-8b48b90c983a', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('dec84de4-bb0d-4c33-af07-55f37051277c', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('c25ecf51-d1e0-4647-8f94-f0ed33ca5e5b', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('17f7e66b-a0fe-446e-ad8f-f5072c683ad2', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('ec3bf5f0-4c84-4c67-b1cd-98e09f751812', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('9e1c590a-5bf4-433f-8967-4bf77afa4d3b', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('d16da805-5024-4cdc-abfa-282eebb7233e', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('ea6d2050-d7d7-4da6-adfd-06e66d7fe88b', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('1b504a5b-678e-4901-b211-ca311d73737e', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('9f66ca60-99ef-408d-9c1c-03fdc094e189', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('84ad09df-75f2-4e4a-8e67-cdb73f8bf587', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2c37deed-8b58-4d73-8b07-bd20da872b5a', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('59514720-0d74-40df-8829-fed49c7cf2f1', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a424050e-0277-4a06-abf9-cd65b885ae90', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('96d7d573-64cd-4026-86f3-a1ef380356cf', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('e8041904-aad5-4372-bc4b-ed0e4cf95021', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2db4cf60-8a87-4907-bd60-8734468f766f', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('746ea7c8-03d4-4c93-8194-6ee65fa72ce4', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a243f9a9-7a8a-4eb5-a1d5-39899ccb56b6', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a18ec48d-6b51-4ba3-bcdb-42afc39383ca', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('dae117d1-f79f-45cc-93e6-69056211a2d3', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('4782d8c9-8bcb-49b0-ad10-89a7ec70579e', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('3b1e9875-9542-4ae2-a36c-b8a1db420b24', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('1c620df8-93f5-414e-bec8-c249aa1ea414', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('257aebc8-b4b3-4956-933c-5ca4f33693e3', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('cb57d07f-0dcd-43a9-a574-b9475db5ff45', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a19f49fe-7f72-41fb-9e99-dcc0b83d16b9', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('52bf98ab-7107-4121-ab38-8523243de5d7', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('3dc5482c-c637-4092-8a67-935b2de1cbca', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('8ff4cb49-0797-4356-ab7e-8d1ccee3c5e4', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('d53c6e60-132b-457c-b637-4115a23405c8', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('22b335a6-26d9-492a-a937-ad771f5fa70e', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('2882ac3f-2873-4855-8ae1-1502491348ae', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('4c3bfdf3-d6b3-4abb-9323-4e0f91e122a1', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('0bf4193d-4393-46cf-b19f-1f3d4d182b06', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('42e11cb0-d094-456a-a655-ee7027c81f96', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('a67130e8-123f-40b6-8131-29226efb87ff', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('51e753fd-e993-4d13-bec0-9108bf5b6678', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('0001eb44-25c3-4eba-b6b1-4c08fedbfcdf', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('290be81a-aca5-4f8e-8ec8-5a65b56c69b5', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('e59f7de3-fcb3-4864-9689-67b8021507b5', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('5020f816-2d25-42da-8314-f16d3780cd74', 5, 42);
INSERT INTO [dbo].[PeriodicAlertConfigurations] ([DeviceId], [NumberOfMinutes], [ThresholdConsumption]) VALUES ('7d39d562-fba4-4c3b-9064-b3d5ce0843e5', 5, 42);
GO
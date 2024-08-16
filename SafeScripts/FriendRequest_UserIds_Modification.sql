use ReadRealm
GO

IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_Name = 'FriendRequests'))
BEGIN
	IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='PK_FriendRequest_SenderUserId_ReceiverUserId'))
	BEGIN
		ALTER TABLE FriendRequests
		DROP CONSTRAINT PK_FriendRequest_SenderUserId_ReceiverUserId
	END

	IF (EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'SenderUserId' AND Object_ID = Object_ID(N'FriendRequests')))
	BEGIN
		ALTER TABLE FriendRequests
		ALTER COLUMN SenderUserId VARCHAR(50) NOT NULL
	END

	IF (EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'ReceiverUserId' AND Object_ID = Object_ID(N'FriendRequests')))
	BEGIN
		ALTER TABLE FriendRequests
		ALTER COLUMN ReceiverUserId VARCHAR(50) NOT NULL
	END

	IF (NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='PK_FriendRequest_SenderUserId_ReceiverUserId'))
	BEGIN
		ALTER TABLE FriendRequests
		ADD CONSTRAINT PK_FriendRequest_SenderUserId_ReceiverUserId PRIMARY KEY (SenderUserId, ReceiverUserId)
	END
END
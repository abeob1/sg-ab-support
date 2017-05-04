--sp_CreateNewUser 'frank','frank@a.com'
alter proc sp_CreateNewUser
@LoginID nvarchar(100),
@EmailID nvarchar(100),
@EmpID int,
@Manager int,
@DefaultWhs nvarchar(8)
as


declare @userid uniqueidentifier
set @userid=NEWID()
insert into aspnet_Users
select (select top(1) ApplicationId from aspnet_Applications),
@userid,@LoginID,@LoginID,null,0,getdate(),@EmpID,@Manager,@DefaultWhs

insert into aspnet_UsersInRoles
select @userid,(select top(1) RoleID from aspnet_Roles)

insert aspnet_Membership 
select (select top(1) ApplicationId from aspnet_Applications),@userid,'Y46uHJ/Qzr0eaH+Sqri/vVLdVuA=',1,'1BLKXgmfmrSRT113EUg7FQ==',
null,@EmailID,@EmailID,null,null,1,0,GETDATE(),GETDATE(),GETDATE(),GETDATE(),0,GETDATE(),0,GETDATE(),null

insert into Users_Default
select @LoginID,'SAPConnection','SBODemoSG;manager;1111;FRANK;sa;sap;FRANK;2008',1




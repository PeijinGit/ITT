```sql
用户注册
CREATE PROC UserRegister 
@username varchar(20),
@pwd varchar(20),
@atype int
AS
INSERT INTO Users (UserName,Password,AccountType) VALUES(@username,@pwd,@atype)
SELECT IDENT_INCR('Users') 
Go

第三方用户登录
CREATE PROC ValidateLogin 
@username varchar(20),
@pwd varchar(20)
AS
SELECT * FROM Users
WHERE Users.UserName = @username AND Users.Password = @pwd
go

更新事件
CREATE PROC UpdateEvent 
@id varchar(25),
@eventName varchar(25)
AS
UPDATE Events SET EventName = @eventName WHERE Id = @id
SELECT IDENT_INCR('Events') 
Go

删除事件并返回受影响行数
CREATE PROC DeleteEvent
@id varchar(30)
AS
DELETE FROM Events
WHERE Events.Id = @id
SELECT @@ROWCOUNT AS rowNum

ALTER PROC DeleteEvent
@id varchar(30)
AS
Begin Transaction
BEGIN TRY
	DELETE FROM EventEnroll WHERE EventEnroll.EventId = @id
	DELETE FROM Events WHERE Events.Id = @id
	SELECT @@rowcount AS rowNum
END TRY
BEGIN CATCH
     ROLLBACK Transaction
END CATCH
IF(@@TRANCOUNT>0)
BEGIN
     COMMIT Transaction
END

添加事件并开启事务更新Enroll表

CREATE PROC AddEvents
@creatorId int,
@eventName varchar(50),
@id varchar(25)
AS
Begin Transaction
BEGIN TRY
     INSERT INTO dbo.Events(Id,EventName,CreatorId,CreateTime) VALUES(@id,@eventName,@creatorId,getdate())
     INSERT INTO dbo.EventEnroll(UserId,EventId)VALUES(@creatorId,@id)
END TRY
BEGIN CATCH
     ROLLBACK Transaction
END CATCH
IF(@@TRANCOUNT>0)
BEGIN
     COMMIT Transaction
END


```

```sql

查看所有储存过程
https://www.cnblogs.com/Bokeyan/p/10983221.html
SELECT name, definition
FROM sys.sql_modules AS m
INNER JOIN sys.all_objects AS o ON m.object_id = o.object_id
WHERE o.[type] = 'P'
```

```sql
EXEC ValidateLogin 'Jin','1231'
```

```sql
CREATE PROC ShowSales
AS
SELECT * FROM EMP WHERE DEPTNO=(
SELECT DEPTNO FROM DEPT WHERER DNAME='sales' 
)
GO
```


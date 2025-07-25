UPDATE AspNetUsers
SET 
    UserStatus = 0,        
    UserRole = 'Admin'
WHERE Email = 'adminuser@gmail.com'; 

SELECT Id, Email, UserName, UserRole, UserStatus 
FROM AspNetUsers
WHERE Email = 'adminuser@gmail.com';

UPDATE AspNetUsers
SET UserName = Email
WHERE UserName IS NULL AND Email IS NOT NULL;

SELECT u.Id, u.UserName, r.Name AS Role
FROM AspNetUsers u
JOIN AspNetUserRoles ur ON u.Id = ur.UserId
JOIN AspNetRoles r ON ur.RoleId = r.Id
WHERE u.UserName = 'anjalee362@gmail.com';
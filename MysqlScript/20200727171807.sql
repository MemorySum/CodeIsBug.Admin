/*
MySQL Backup
Database: codeisbug.admin
Backup Time: 2020-07-27 17:18:07
*/

SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS `codeisbug.admin`.`e_base_emp`;
DROP TABLE IF EXISTS `codeisbug.admin`.`e_sys_emprolemap`;
DROP TABLE IF EXISTS `codeisbug.admin`.`e_sys_menu`;
DROP TABLE IF EXISTS `codeisbug.admin`.`e_sys_role`;
CREATE TABLE `e_base_emp` (
  `UserId` int NOT NULL AUTO_INCREMENT COMMENT '用户Id',
  `Name` varchar(100) NOT NULL COMMENT '用户姓名',
  `UserName` varchar(100) NOT NULL COMMENT '账号',
  `UserAvatarUrl` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '用户头像',
  `Phone` varchar(100) DEFAULT NULL COMMENT '手机号',
  `Email` varchar(100) DEFAULT NULL COMMENT '邮箱',
  `Pwd` varchar(100) NOT NULL COMMENT '密码',
  `IsDelete` int NOT NULL COMMENT '是否删除',
  `AddTime` datetime NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
CREATE TABLE `e_sys_emprolemap` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `EmpId` int DEFAULT NULL COMMENT '用户Id',
  `RoleId` int DEFAULT NULL COMMENT '角色Id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
CREATE TABLE `e_sys_menu` (
  `MenuId` int NOT NULL AUTO_INCREMENT COMMENT '菜单Id',
  `ParentId` int DEFAULT NULL COMMENT '父级Id',
  `Name` varchar(100) NOT NULL COMMENT '菜单名称',
  `Url` varchar(100) DEFAULT NULL COMMENT '菜单地址',
  `Icon` varchar(100) DEFAULT NULL COMMENT '菜单图标',
  `Sort` int NOT NULL COMMENT '菜单排序',
  `Level` int NOT NULL COMMENT '菜单层级',
  `IsDeleted` int DEFAULT NULL COMMENT '是否删除',
  `AddTime` datetime NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`MenuId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
CREATE TABLE `e_sys_role` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT '角色Id',
  `ParentId` int NOT NULL COMMENT '父级Id',
  `Name` varchar(100) NOT NULL COMMENT '角色名称',
  `Sort` int NOT NULL COMMENT '角色排序',
  `Remark` varchar(100) DEFAULT NULL COMMENT '备注',
  `AddTime` datetime NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
BEGIN;
LOCK TABLES `codeisbug.admin`.`e_base_emp` WRITE;
DELETE FROM `codeisbug.admin`.`e_base_emp`;
INSERT INTO `codeisbug.admin`.`e_base_emp` (`UserId`,`Name`,`UserName`,`UserAvatarUrl`,`Phone`,`Email`,`Pwd`,`IsDelete`,`AddTime`,`ModifyTime`) VALUES (1, '系统管理员', 'admin', NULL, '13545698778', '1@qq.com', 'c4ca4238a0b923820dcc509a6f75849b', 0, '2020-07-26 22:46:28', '2020-07-26 22:46:47');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `codeisbug.admin`.`e_sys_emprolemap` WRITE;
DELETE FROM `codeisbug.admin`.`e_sys_emprolemap`;
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `codeisbug.admin`.`e_sys_menu` WRITE;
DELETE FROM `codeisbug.admin`.`e_sys_menu`;
INSERT INTO `codeisbug.admin`.`e_sys_menu` (`MenuId`,`ParentId`,`Name`,`Url`,`Icon`,`Sort`,`Level`,`IsDeleted`,`AddTime`,`ModifyTime`) VALUES (1, 0, '系统管理', NULL, 'el-icon-setting', 1, 0, 0, '2020-07-26 23:11:37', NULL),(2, 1, '菜单管理', '/sysmenu', 'el-icon-s-grid', 1, 1, 0, '2020-07-26 23:12:29', NULL),(4, 1, '用户管理', '/sysUsers', 'el-icon-s-custom', 2, 1, 0, '2020-07-26 23:43:07', NULL);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `codeisbug.admin`.`e_sys_role` WRITE;
DELETE FROM `codeisbug.admin`.`e_sys_role`;
UNLOCK TABLES;
COMMIT;

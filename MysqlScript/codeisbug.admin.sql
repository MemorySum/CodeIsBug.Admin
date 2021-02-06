/*
 Navicat Premium Data Transfer

 Source Server         : 本地
 Source Server Type    : MySQL
 Source Server Version : 80022
 Source Host           : localhost:3306
 Source Schema         : codeisbug.admin

 Target Server Type    : MySQL
 Target Server Version : 80022
 File Encoding         : 65001

 Date: 22/12/2020 16:44:30
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for e_base_emp
-- ----------------------------
DROP TABLE IF EXISTS `e_base_emp`;
CREATE TABLE `e_base_emp`  (
  `UserId` int(0) NOT NULL AUTO_INCREMENT COMMENT '用户Id',
  `Name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户姓名',
  `UserName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账号',
  `UserAvatarUrl` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户头像',
  `Phone` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Pwd` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `IsDelete` int(0) NOT NULL COMMENT '是否删除',
  `AddTime` datetime(0) NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`UserId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of e_base_emp
-- ----------------------------
INSERT INTO `e_base_emp` VALUES (1, '系统管理员', 'admin', NULL, '13545698778', '1@qq.com', 'c4ca4238a0b923820dcc509a6f75849b', 0, '2020-07-26 22:46:28', '2020-07-26 22:46:47');

-- ----------------------------
-- Table structure for e_sys_emprolemap
-- ----------------------------
DROP TABLE IF EXISTS `e_sys_emprolemap`;
CREATE TABLE `e_sys_emprolemap`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT COMMENT '主键Id',
  `EmpId` int(0) NULL DEFAULT NULL COMMENT '用户Id',
  `RoleId` int(0) NULL DEFAULT NULL COMMENT '角色Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of e_sys_emprolemap
-- ----------------------------

-- ----------------------------
-- Table structure for e_sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `e_sys_menu`;
CREATE TABLE `e_sys_menu`  (
  `MenuId` int(0) NOT NULL AUTO_INCREMENT COMMENT '菜单Id',
  `ParentId` int(0) NULL DEFAULT NULL COMMENT '父级Id',
  `Name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '菜单名称',
  `Url` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单地址',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单图标',
  `Sort` int(0) NOT NULL COMMENT '菜单排序',
  `Level` int(0) NOT NULL COMMENT '菜单层级',
  `IsDeleted` int(0) NULL DEFAULT NULL COMMENT '是否删除',
  `AddTime` datetime(0) NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`MenuId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of e_sys_menu
-- ----------------------------
INSERT INTO `e_sys_menu` VALUES (1, 0, '菜单管理', '/sysmenu', 'el-icon-s-grid', 1, 0, 0, '2020-07-26 23:12:29', NULL);
INSERT INTO `e_sys_menu` VALUES (2, 0, '用户管理', '/sysUsers', 'el-icon-s-custom', 2, 0, 0, '2020-07-26 23:43:07', NULL);
INSERT INTO `e_sys_menu` VALUES (3, 1, '菜单列表', NULL, NULL, 3, 1, 0, '2020-12-22 16:43:48', NULL);
INSERT INTO `e_sys_menu` VALUES (4, 2, '用户列表', NULL, NULL, 4, 1, 0, '2020-12-22 16:44:08', NULL);

-- ----------------------------
-- Table structure for e_sys_role
-- ----------------------------
DROP TABLE IF EXISTS `e_sys_role`;
CREATE TABLE `e_sys_role`  (
  `Id` int(0) NOT NULL AUTO_INCREMENT COMMENT '角色Id',
  `ParentId` int(0) NOT NULL COMMENT '父级Id',
  `Name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色名称',
  `Sort` int(0) NOT NULL COMMENT '角色排序',
  `Remark` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `AddTime` datetime(0) NOT NULL COMMENT '添加时间',
  `ModifyTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of e_sys_role
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;

--本地测试需要执行此脚本，上线时甲方DB中应该存在该表及对应数据
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (1, '西安中心', 'XSCC', '02 60', '02 60 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (2, '空间信息综合应用中心', 'XXZX', '02 6F', '02 6F 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (3, '运控评估中心', 'YKZX', '02 04', '02 04 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (4, 'S地面站-喀什站（TW-217）', null, '02 C3', '02 C3 00 10');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (5, 'S地面站-厦门站（TW-218）', null, '02 C6', '02 C6 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (6, 'X地面站-青岛站（TY-4801）', null, '02 C5', '02 C5 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (7, 'X地面站-喀什站（TY-4801）', null, '02 C3', '02 C3 00 20');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (8, 'X地面站-瑞典站', null, '02 48', '02 48 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (9, 'X地面站-总参二部信息处理中心', 'XXZX', '02 B0', '02 B0 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (10, 'X地面站-总参二部牡丹江站', null, '02 B1', '02 B1 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (11, 'X地面站-总参三部技侦中心', 'JZZX', '02 B2', '02 B2 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (12, 'X地面站-总参三部长春站', null, '02 B3', '02 B3 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (13, 'X地面站-总参三部乌鲁木齐站', null, '02 B4', '02 B4 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (14, 'X地面站-总参三部广州站', null, '02 B5', '02 B5 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (15, 'X地面站-总参气象水文空间天气总站' || chr(10) || '资料处理中心' || chr(10) || '', 'ZLZX', '02 B6', '02 B6 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (16, 'X地面站-总参气象水文空间天气总站' || chr(10) || '北京站' || chr(10) || '', null, '02 B7', '02 B7 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (17, '遥科学综合站-东风站（TW-218）', 'DFYZ', '02 29', '02 29 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (18, '遥科学综合站-863-YZ4701遥科学综合站', 'JYZ1', '02 BA', '02 BA 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (19, '遥科学综合站-863-YZ4702遥科学综合站', 'JYZ2', '02 BC', '02 BC 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (20, '天基目标观测应用研究分系统', 'GCYJ', '02 E1', '02 E1 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (21, '空间遥操作应用研究分系统', 'CZYJ', '02 E3', '02 E3 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (22, '空间机动应用研究分系统', 'JDYJ', '02 E5', '02 E5 00 00');
insert into TB_XYXSINFO (rid, "ADDRName", addrmark, incode, excode)
values (23, '仿真推演分系统', 'FZTY', '02 E7', '02 E7 00 00');
commit;
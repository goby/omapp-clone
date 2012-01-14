--本地测试需要执行此脚本，上线时甲方DB中应该存在该表及对应数据
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (1, '试验需求', 'OR_SYXQ', 'SYXQ', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (2, '空间目标信息', 'OR_KJMB', 'MBXX', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (3, '空间环境信息', 'OR_KJHJ', 'HJXX', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (4, '交会预报报告', 'OR_JHBG', 'JHBG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (5, '碰撞预警报告', 'OR_YJBG', 'YJBG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (6, '天基目标观测试验数据处理结果', 'OR_TJJG', 'GCJG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (7, '空间遥操作试验数据处理结果', 'OR_YCZJG', 'CZJG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (8, '空间机动试验数据处理结果', 'OR_JDJG', 'JDJG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (9, '仿真推演试验数据处理结果', 'OR_TYJG', 'TYJG', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (10, '天基目标观测试验数据', 'CQ_TJSY', 'GCSJ', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (11, '空间机动试验数据', 'CQ_JDSY', 'JDSJ', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (12, '地面站工作计划', 'GL_DMJH', 'GZJH', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (13, '应用研究工作计划', 'GL_YJJH', 'YJJH', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (14, '仿真推演试验数据', 'GL_TYSJ', 'TYSJ', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (15, '空间信息需求', 'GL_XXXQ', 'XXXQ', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (16, '设备工作计划', 'GL_SBJH', 'SBJH', null, null, 1);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (17, '原始数传数据', 'OR_YSSC', 'SCSJ', '515000H', '00 11 02 02', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (18, '原始遥测数据', 'OR_YCYC', 'XY', '515001H', '00 11 01 11', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (19, '遥控执行情况', 'OR_YKZX', 'YKZX', '515002H', '00 21 01 06', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (20, '遥操作控制数据-任务级', 'OR_YCZ1', 'YCZ1', '515003H', '00 00 AA 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (21, '遥操作控制数据-遥编程', 'OR_YCZ2', 'YCZ2', '515013H', '00 00 AB 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (22, '遥操作控制数据-主从双边', 'OR_YCZ3', 'YCZ3', '515023H', '00 00 AC 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (23, '系统运行状态', 'OR_YXZT', 'XTZT', '515004H', '00 70 03 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (24, '小环比对', 'OR_XHBD', 'XHBD', '515005H', '00 21 01 04', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (25, '测距数据', 'OR_CRSJ', 'R', '515006H', '00 10 06 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (26, '测距数据', 'OR_CTSJ', 'AE', '515007H', '00 10 06 05', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (27, '测速数据', 'OR_CSSJ', 'OR', '515008H', '00 10 06 03', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (28, '角度跟踪信息', 'OR_JDGZ', 'AE', '515012H', '00 10 06 05', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (29, '远程监控数据', 'OR_YCJK', 'YCJK', '515009H', '00 00 F0 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (30, '相对定位数据', 'OR_XDDW', 'XDDW', '51500AH', '00 00 30 4F', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (31, '地心系弹道数据-理论弹道数据', 'OR_DXDD', 'r', '515070H', '00 2A 01 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (32, '地心系弹道数据-综合飞行弹道', 'OR_DXDD', 'r', '515071H', '00 2A 01 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (33, '地心系弹道数据-外测综合飞行弹道', 'OR_DXDD', 'r', '515072H', '00 2A 01 02', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (34, '地心系弹道数据-外测合作式飞行弹道', 'OR_DXDD', 'r', '515073H', '00 2A 01 03', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (35, '地心系弹道数据-外测非合作式飞行弹道', 'OR_DXDD', 'r', '515074H', '00 2A 01 04', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (36, '地心系弹道数据-遥测综合飞行弹道', 'OR_DXDD', 'r', '515075H', '00 2A 01 05', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (37, '地心系弹道数据-GPS弹道', 'OR_DXDD', 'r', '515076H', '00 2A 01 06', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (38, '地心系弹道数据-GPS自定位弹道', 'OR_DXDD', 'r', '515077H', '00 2A 01 07', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (39, '试验T0', 'OR_SYT0', 'T0', '51500CH', '00 01 01 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (40, '卫星初始轨道根数', 'OR_CSGS', 'GD', '51501AH', '00 2A 02 02', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (41, '卫星瞬时精轨根数', 'OR_SJGS', 'GD', '51501BH', '00 2A 02 03', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (42, '卫星事后精轨根数', 'OR_SHJG', 'GDSH', '51501CH', '00 2A 02 04', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (43, '星地时差', 'OR_XDSC', 'XDSC', '515010H', '00 00 BF 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (44, '链监', 'OR_LLJS', 'LJ', '510101H', '00 02 01 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (45, '回答', 'GL_XXHD', 'HD', '300200H', '00 05 01 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (46, '信息结束', 'GL_XXJS', 'XXJS', '300201H', '00 05 01 02', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (47, '指令/数据注入申请', 'GL_ZZSQ', 'ZRSJ', '300300H', '00 21 01 02', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (48, '遥控工作期设置', 'GL_GZQ', 'GZQ', '300301H', '00 21 01 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (49, '指令/数据注入', 'GL_GZQ', 'ZLSQ', '300302H', '00 21 04 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (50, '时延测量数据', 'GL_SYCL', 'SYCL', '30030AH', '00 21 04 01', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (51, '空间机动状态数据', 'CQ_JDZT', 'JDZT', '451300H', '00 70 26 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (52, '空间机动测量数据', 'CQ_JDCL', 'JDCL', '451300H', '00 70 27 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (53, '空间遥操作状态数据', 'CQ_YCZZT', 'CZZT', '451304H', '00 70 24 00', 0);
insert into TB_XXTYPE (rid, dataname, inmark, exmark, incode, excode, datatype)
values (54, '空间遥操作图像数据', 'CQ_YCZTX', 'CZTX', '451305H', '00 70 25 00', 0);
commit;
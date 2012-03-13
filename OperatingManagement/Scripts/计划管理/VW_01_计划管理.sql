---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/3/13, 21:36:03 --
---------------------------------------------

spool VW_01_计划管理20120313.log

prompt
prompt Creating view V_GD
prompt ==================
prompt
create or replace view htcuser.v_gd as
select g.id,g.ctime gdctime,g.reserve,g.satid,g.itype,g.icode,g.d,g.t,g.times,g.a,g.e,g.i,g.q,g.w,g.m,
       g.p,g.deltp,g.ra,g.rp,g.cdsm,g.kz1,g.ksm,g.kz2,b.ctime,b.version,b.flag,b.maintype,b.datatype,b.sourceaddress,
       b.destinationaddress,b.missioncodev,b.satellitecode,b.datadate,b.datatime,b.sequencenumber,
       b.childrenpacknumber,b.udpreserve,b.datalength,b.dataclass,b.tb_table
from tb_gd g,tb_basicinfodata b
where g.id = b.id(+);

prompt
prompt Creating view V_YDSJ
prompt ====================
prompt
create or replace view htcuser.v_ydsj as
select g.id,g.ctime ydsjctime,g.reserve,g.data_d,g.data_t,g.data_a,g.data_e,g.data_i,g.data_ohm,g.data_omega,g.data_m,
       b.ctime,b.version,b.flag,b.maintype,b.datatype,b.sourceaddress,
       b.destinationaddress,b.missioncodev,b.satellitecode,b.datadate,b.datatime,b.sequencenumber,
       b.childrenpacknumber,b.udpreserve,b.datalength,b.dataclass,b.tb_table
from tb_ydsj g,tb_basicinfodata b
where g.data_id = b.id(+);


spool off

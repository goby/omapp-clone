---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/7/11, 22:15:08 --
---------------------------------------------

spool UP_01_计划管理20120711.log

prompt
prompt Creating procedure UP_GD_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.up_GD_Getlist
(
       p_startTime in TB_GD.Ctime%type,
       p_endTime in TB_GD.Ctime%type,
       o_cursor out sys_refcursor
) is
v_sql varchar2(4000);
begin

v_sql:='SELECT t.*, case when k.taskname  is null then t.taskid else k.taskname end taskname,'||
          '  case when s.WXMC  is null then t.satid else s.WXMC end WXMC '||
          'FROM TB_GD t,TB_TASK k,tb_satellite s '||
           ' where t.taskid=k.taskno(+) and t.satid=s.WXBM(+) ';

   if (p_startTime is not null)
   then
      v_sql:=v_sql||' and t.ctime >= '''|| p_startTime ||'''';
   end if;
   if (p_endTime is not null)
   then
      v_sql:=v_sql||' and t.ctime <= '''|| p_endTime ||'''';
   end if;

   OPEN o_cursor For v_sql;

end up_GD_Getlist;
/

prompt
prompt Creating procedure UP_GD_INSERT
prompt ===============================
prompt
create or replace procedure htcuser.up_GD_insert
(
       p_CTime tb_GD.CTime%type,
       p_TaskID tb_GD.TaskID%type,
       p_Satid tb_GD.Satid%type,
       p_IType tb_GD.Itype%type,
       p_ICode tb_GD.Icode%type,
       p_D tb_GD.d%type,
       p_T tb_GD.t%type,
       p_Times tb_GD.Times%type,
       p_A tb_GD.a%type,
       p_E tb_GD.e%type,
       p_I tb_GD.i%type,
       p_Q tb_GD.q%type,
       p_W tb_GD.w%type,
       p_M tb_GD.m%type,
       p_P tb_GD.p%type,
       p_PP tb_GD.pp%type,
       p_RA tb_GD.Ra%type,
       p_RP tb_GD.Rp%type,
       p_CDSM tb_GD.cdsm%type,
       p_KSM tb_GD.ksm%type,
       p_KZ1 tb_GD.kz1%type,
       p_KZ2 tb_GD.kz2%type,
       p_Reserve tb_GD.Reserve%type,
       p_DFInfoID tb_GD.DFInfoID%type,
       v_Id out tb_GD.Id%type,
       v_result out number
)
is
begin
       select seq_tb_gd.NEXTVAL INTO v_Id from dual;
       insert into tb_gd (ID, CTIME, TaskID, satid, itype, icode, D, T, times, A, E, I, Q, W, M, P, pp, RA, RP, cdsm, kz1, ksm, kz2, RESERVE, DFInfoID)
       values(v_Id, p_CTime, p_TaskID, p_Satid, p_IType, p_ICode, p_D, p_T, p_Times, p_A, p_E, p_I, p_Q, p_W, p_M, p_P, p_PP, p_RA, p_RP, p_CDSM, p_KZ1, p_KSM, p_KZ2, p_Reserve, p_DFInfoID);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_GD_insert;
/

prompt
prompt Creating procedure UP_GD_SELECTBYID
prompt ===================================
prompt
create or replace procedure htcuser.up_gd_selectByID
(
       p_Id tb_gd.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
       SELECT t.*, case when k.taskname  is null then t.taskid else k.taskname end taskname,
       case when s.WXMC  is null then t.satid else s.WXMC end WXMC 
       FROM TB_GD t,TB_TASK k,tb_satellite s 
        where t.taskid=k.taskno(+) and t.satid=s.WXBM(+) and t.id=p_Id;
end;
/

prompt
prompt Creating procedure UP_JH_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.UP_JH_GETLIST
(
       p_planType IN TB_JH.Plantype%type Default null,
       p_startDate IN TB_JH.Ctime%type Default null,
       p_endDate  IN TB_JH.Ctime%type Default null,
       o_cursor out sys_refcursor
)
is
v_sql varchar2(4000);
begin
  v_sql:='SELECT t.*,k.taskname '||
          ' FROM TB_JH t,TB_TASK k '||
          ' where t.taskid=k.taskno(+) ';
   if (p_planType is not null)
   then
      v_sql:=v_sql||' and t.plantype = '''|| p_planType ||'''';
   end if;
   if (p_startDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME >=to_date( '''|| to_char(p_startDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME <=to_date( '''|| to_char(p_endDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
  --    v_sql:=v_sql||' and t.CTIME <= '''|| p_endDate ||'''';
   end if;
--dbms_output.put_line(v_sql);
   OPEN o_cursor For v_sql;

end;
/

prompt
prompt Creating procedure UP_JH_GETSYJHLIST
prompt ====================================
prompt
create or replace procedure htcuser.UP_JH_GETSYJHLIST
(
       p_startDate IN TB_JH.Ctime%type Default null,
       p_endDate  IN TB_JH.Ctime%type Default null,
       o_cursor out sys_refcursor
)
is
v_sql varchar2(4000);
begin
    v_sql:='SELECT t.*,k.taskname '||
          ' FROM TB_JH t,TB_TASK k '||
          ' where  t.srctype=1 and  t.taskid=k.taskno(+) ';
   if (p_startDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME >=to_date( '''|| to_char(p_startDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME <=to_date( '''|| to_char(p_endDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;
--dbms_output.put_line(v_sql);
   OPEN o_cursor For v_sql;

end;
/

prompt
prompt Creating procedure UP_JH_INSERT
prompt ===============================
prompt
create or replace procedure htcuser.up_jh_insert
      (
      p_TaskID tb_jh.taskid%type,
      p_PlanType tb_jh.plantype%type,
      p_PlanID tb_jh.planid%type,
      p_StartTime tb_jh.starttime%type,
      p_EndTime tb_jh.endtime%type,
      p_SRCType tb_jh.srctype%type,
      p_SRCID tb_jh.srcid%type,
      p_FileIndex tb_jh.fileindex%type,
      p_Reserve tb_jh.reserve%type,
       v_Id out tb_jh.Id%type,
       v_result out number
)
is
begin
       select seq_tb_JH.NEXTVAL INTO v_Id from dual;
       insert into tb_jh(id,ctime,taskid,plantype, planid,starttime,endtime,srctype,srcid,fileindex,reserve)
       values(v_Id,sysdate(),p_TaskID,p_PlanType,p_PlanID,p_StartTime,p_EndTime,p_SRCType,p_SRCID,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end;
/

prompt
prompt Creating procedure UP_JH_SELECTBYPLANTYPEANDID
prompt ==============================================
prompt
create or replace procedure htcuser.up_jh_selectbyplantypeandid
(
       p_PlanType tb_jh.plantype%type,
       p_PlanId tb_jh.planid%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_jh where plantype=p_PlanType and planid=p_PlanId;
end;
/

prompt
prompt Creating procedure UP_JH_SELECTINIDS
prompt ====================================
prompt
create or replace procedure htcuser.up_jh_SelectInIDS
(
       p_ids varchar2,
       o_cursor out sys_refcursor
) is
v_sql varchar2(4000);
begin
v_sql:='SELECT * FROM TB_JH t '||
          ' where 1=1 ';

   if (p_ids is not null)
   then
      v_sql:=v_sql||' and t.id in ('|| p_ids ||')';
   end if;

   dbms_output.put_line(v_sql);

   OPEN o_cursor For v_sql;


end;
/

prompt
prompt Creating procedure UP_JH_UPDATE
prompt ===============================
prompt
create or replace procedure htcuser.up_jh_update
(
       v_Id  tb_jh.Id%type,
       p_taskid tb_jh.taskid%type,
       p_starttime tb_jh.starttime%type,
       p_endtime tb_jh.endtime%type,
       p_FileIndex tb_jh.fileindex%type,
       v_result out number
)
is
begin
     update   tb_jh t set
     t.taskid = p_taskid,
     t.starttime = p_starttime,
     t.endtime = p_endtime,
     t.fileindex = p_FileIndex
      where t.Id = v_Id;
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end;
/

prompt
prompt Creating procedure UP_JH_UPDATEFILEINDEX
prompt ========================================
prompt
create or replace procedure htcuser.up_jh_updatefileindex
(
       v_Id  tb_jh.Id%type,
       p_FileIndex tb_jh.fileindex%type,
       v_result out number
)
is
begin
     update   tb_jh t set
     t.fileindex=p_FileIndex  where t.Id = v_Id;
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end;
/

prompt
prompt Creating procedure UP_SYCX_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.UP_SYCX_GETLIST
(
       p_startDate IN TB_SYCX.Ctime%type Default null,
       p_endDate  IN TB_SYCX.Ctime%type Default null,
       o_cursor out sys_refcursor
)
is
v_sql varchar2(4000);
begin
  v_sql:='SELECT * FROM TB_SYCX t '||
          ' where 1=1 ';

   if (p_startDate is not null)
   then
      v_sql:=v_sql||' and t.Starttime >=to_date( '''|| to_char(p_startDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.Starttime <=to_date( '''|| to_char(p_endDate,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;

   OPEN o_cursor For v_sql;

end;
/

prompt
prompt Creating procedure UP_SYCX_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.UP_SYCX_SELECTBYID
(
       p_Id tb_sycx.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_sycx where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_YDSJ_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.up_ydsj_Getlist
(
       p_startTime in TB_YDSJ.Ctime%type Default null,
       p_endTime in TB_YDSJ.Ctime%type Default null,
       o_cursor out sys_refcursor
) is
v_sql varchar2(4000);
begin
v_sql:='SELECT t.*, case when k.taskname  is null then t.taskid else k.taskname end taskname  '||
          'FROM TB_YDSJ t,TB_TASK k '||
          ' where t.taskid=k.taskno(+) ';

   if (p_startTime is not null)
   then
      v_sql:=v_sql||' and t.ctime >=to_date( '''|| to_char(p_startTime,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;
   if (p_endTime is not null)
   then
      v_sql:=v_sql||' and t.ctime <=to_date( '''|| to_char(p_endTime,'yyyy/MM/dd HH24:mi:ss') ||''',''yyyy/MM/dd hh24:mi:ss'')';
   end if;

   dbms_output.put_line(v_sql);

   OPEN o_cursor For v_sql;


end up_ydsj_Getlist;
/

prompt
prompt Creating procedure UP_YDSJ_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_ydsj_insert
(
      p_CTime tb_ydsj.CTime%type,
      p_TaskID tb_ydsj.taskid%type,
      p_SatName tb_ydsj.satname%type,
      p_d tb_ydsj.d%type,
      p_t tb_ydsj.t%type,
      p_times tb_ydsj.times%type,
      p_a tb_ydsj.a%type,
      p_e tb_ydsj.e%type,
      p_i tb_ydsj.i%type,
      p_o tb_ydsj.o%type,
      p_w tb_ydsj.w%type,
      p_m tb_ydsj.m%type,
      p_Reserve tb_ydsj.reserve%type,
       v_Id out tb_ydsj.Id%type,
       v_result out number
)
is
begin
       select seq_tb_ydsj.NEXTVAL INTO v_Id from dual;
       insert into tb_ydsj(id, ctime, taskid, SatName, d, t, times, a, e, i, o, w, m, reserve)
       values(v_Id, p_CTime, p_TaskID, p_SatName, p_d, p_t, p_Times, p_a, p_e, p_i, p_o, p_w, p_m, p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end;
/

prompt
prompt Creating procedure UP_YDSJ_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.up_ydsj_selectByID
(
       p_Id tb_ydsj.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_YDSJ where id=p_Id;
end;
/


spool off

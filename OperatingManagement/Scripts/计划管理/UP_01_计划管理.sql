---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/4/14, 23:20:10 --
---------------------------------------------

spool UP_01_计划管理20120414.log

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

v_sql:='SELECT * FROM TB_GD t '||
          ' where 1=1 ';

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
       p_Reserve tb_GD.Reserve%type,
       p_Satid tb_GD.Satid%type,
       p_IType tb_GD.Itype%type,
       p_ICode tb_GD.Icode%type,
       p_Data_D tb_GD.d%type,
       p_Data_T tb_GD.t%type,
       p_Times tb_GD.Times%type,
       p_Data_A tb_GD.a%type,
       p_Data_E tb_GD.e%type,
       p_Data_I tb_GD.i%type,
       p_Data_Ohm tb_GD.q%type,
       p_Data_Omega tb_GD.w%type,
       p_Data_M tb_GD.m%type,
       p_Data_P tb_GD.p%type,
       p_Data_PI tb_GD.Deltp%type,
       p_Data_RA tb_GD.Ra%type,
       p_Data_RP tb_GD.Rp%type,
       p_Data_CDSM tb_GD.cdsm%type,
       p_Data_KSM tb_GD.ksm%type,
       p_Data_KZ1 tb_GD.kz1%type,
       p_Data_KZ2 tb_GD.kz2%type,
       v_Id out tb_GD.Id%type,
       v_result out number
)
is
begin
       select seq_tb_module.NEXTVAL INTO v_Id from dual;
       insert into tb_gd (ID, CTIME, RESERVE,satid,itype,icode, D, T,times, A, E, I, Q, W, M, P,deltp, RA, RP,cdsm,kz1,ksm,kz2)
       values(v_Id,sysdate(),p_Reserve,p_Satid,p_IType,p_ICode,p_Data_D,p_Data_T,p_Times,p_Data_A,p_Data_E,p_Data_I,p_Data_Ohm,p_Data_Omega,p_Data_M,p_Data_P,p_Data_PI,p_Data_RA,p_Data_RP,p_Data_CDSM,p_Data_KZ1,p_Data_KSM,p_Data_KZ2);

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
            select * from tb_GD where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_RESET_SEQUENCE
prompt ====================================
prompt
create or replace procedure htcuser.UP_RESET_SEQUENCE(v_seqname varchar2)
as
 n_temp number(10);
 s_tsql varchar2(100);
begin
 execute immediate ' select ' || v_seqname || '.nextval from dual' into n_temp;

 if n_temp <> 1 then
  n_temp := -(n_temp-1);
  s_tsql := ' alter sequence ' || v_seqname || ' increment by ' || n_temp;
  execute immediate s_tsql;
  execute immediate ' select ' || v_seqname || '.nextval from dual' into n_temp;
  s_tsql := ' alter sequence ' || v_seqname || ' increment by 1 ';
  execute immediate s_tsql;
   s_tsql :='update tb_sequencenumber set currentvalue =0 where sequencename=''' || v_seqname||'''';
   execute immediate s_tsql;
  
 end if;
end;
/

prompt
prompt Creating procedure UP_GEN_SEQUENCE
prompt ==================================
prompt
create or replace procedure htcuser.UP_Gen_SEQUENCE
(
       p_seqname varchar2,
       o_seqnum out number
)
as
 
begin
  execute immediate ' select ' || p_seqname || '.nextval from dual' into o_seqnum;

if o_seqnum >= 9999 then
  up_reset_sequence (p_seqname);
end if;

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
  v_sql:='SELECT * '||
          ' FROM TB_JH t '||
          ' where 1=1 ';
   if (p_planType is not null)
   then
      v_sql:=v_sql||' and t.plantype = '''|| p_planType ||'''';
   end if;
   if (p_startDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME >= '''|| p_startDate ||'''';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME <= '''|| p_endDate ||'''';
   end if;
dbms_output.put_line(v_sql);
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
  v_sql:='SELECT * '||
          ' FROM TB_JH t '||
          ' where t.srctype=1 ';
   if (p_startDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME >= '''|| p_startDate ||'''';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.CTIME <= '''|| p_endDate ||'''';
   end if;
dbms_output.put_line(v_sql);
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
       select seq_tb_module.NEXTVAL INTO v_Id from dual;
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
       v_result out number
)
is
begin
     update   tb_jh t set
     t.taskid = p_taskid,
     t.starttime = p_starttime,
     t.endtime = p_endtime
      where t.id = v_Id;
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
v_sql:='SELECT * FROM TB_YDSJ t '||
          ' where 1=1 ';

   if (p_startTime is not null)
   then
      v_sql:=v_sql||' and t.ctime >= '''|| p_startTime ||'''';
   end if;
   if (p_endTime is not null)
   then
      v_sql:=v_sql||' and t.ctime <= '''|| p_endTime ||'''';
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
       insert into tb_ydsj(id,ctime,taskid,satname,d,t,a,e,i,o,w,m,reserve)
       values(v_Id,p_CTime,p_TaskID,p_SatName,p_d,p_t,p_a,p_e,p_i,p_o,p_w,p_m,p_Reserve);
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

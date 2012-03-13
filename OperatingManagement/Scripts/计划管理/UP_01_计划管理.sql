---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/3/13, 21:35:04 --
---------------------------------------------

spool UP_01_计划管理20120313.log

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
            select * from v_GD where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_GEN_SEQUENCE
prompt ==================================
prompt
create or replace procedure htcuser.UP_Gen_SEQUENCE
(
       p_seqtype varchar2,
       p_seqname varchar2,
       o_seqnum out number
)
as
 
begin
o_seqnum := to_number(fn_genseqnum(p_seqtype));

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
       v_Id:= to_number(fn_genseqnum('0040'));
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
      v_sql:=v_sql||' and t.Starttime >= '''|| p_startDate ||'''';
   end if;
   if (p_endDate is not null)
   then
      v_sql:=v_sql||' and t.Starttime <= '''|| p_endDate ||'''';
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
prompt Creating procedure UP_SYJH_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.UP_SYJH_GETLIST
(
       p_startDate IN TB_SYJH.Ctime%type Default null,
       p_endDate  IN TB_SYJH.Ctime%type Default null,
       o_cursor out sys_refcursor
)
is
v_sql varchar2(4000);
begin
  v_sql:='SELECT '||
         't.JHID,'||
         't.CTIME,'||
          't.Source,'||
          't.Destination,'||
          't.Infotype,'||
          't.Linecount,'||
          't.TASKID,'||
          't.PLANTYPE,'||
          't.PLANID,'||
          't.STARTTIME,'||
          't.ENDTIME,'||
          't.FILEINDEX,'||
          't.RESERVE '||
          ' FROM TB_SYJH t '||
          ' where 1=1 ';

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
prompt Creating procedure UP_SYJH_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.UP_SYJH_SELECTBYID
(
       p_Id tb_syjh.jhid%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_syjh where jhid=p_Id;
end;
/

prompt
prompt Creating procedure UP_YDSJ_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.up_ydsj_Getlist
(
       p_sapceType in TB_YDSJ.Spacetype%type,
       p_startTime in TB_YDSJ.Ctime%type Default null,
       p_endTime in TB_YDSJ.Ctime%type Default null,
       o_cursor out sys_refcursor
) is
v_sql varchar2(4000);
begin
v_sql:='SELECT * FROM TB_YDSJ t '||
          ' where 1=1 ';

   v_sql :=v_sql||' and t.spacetype = '''|| p_sapceType || '''';
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


spool off

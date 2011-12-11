----------------------------------------------
-- Export file for user HTCUSER             --
-- Created by taiji on 2011/12/11, 21:59:13 --
----------------------------------------------

spool UP_01_计划管理.log

prompt
prompt Creating procedure UP_GZJH_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_GZJH_insert
(
       p_CTime tb_GZJH.Ctime%type,
       p_Source tb_GZJH.Source%type,
       p_Destination tb_GZJH.Destination%type,
       p_TaskID tb_GZJH.Taskid%type,
       p_InfoType tb_GZJH.Infotype%type,
       p_LineCount tb_GZJH.Linecount%type,
       p_Format1 tb_GZJH.format1%type,
       p_Format2 tb_GZJH.format2%type,
       p_DataSection tb_GZJH.datasection%type,
       p_FileIndex tb_GZJH.Fileindex%type,
       p_Reserve tb_GZJH.Reserve%type,
       v_Id out tb_GZJH.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0043'));
       insert into tb_GZJH(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Format1,Format2,datasection,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_Format1,p_Format1,p_DataSection,p_FileIndex,p_Reserve);
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
prompt Creating procedure UP_GZJH_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.up_GZJH_selectbyid
(
       p_Id tb_gzjh.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_gzjh where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_GZJH_UPDATE
prompt =================================
prompt
create or replace procedure htcuser.up_GZJH_update
(
       v_Id out tb_GZJH.Id%type,
       p_Source tb_GZJH.Source%type,
       p_Destination tb_GZJH.Destination%type,
       p_TaskID tb_GZJH.Taskid%type,
       p_InfoType tb_GZJH.Infotype%type,
       p_LineCount tb_GZJH.Linecount%type,
       p_Format1 tb_GZJH.format1%type,
       p_Format2 tb_GZJH.format2%type,
       p_DataSection tb_GZJH.datasection%type,
       p_Reserve tb_GZJH.Reserve%type,
       v_result out number
)
is
begin
     update   tb_GZJH set
     Source=p_Source,
     Destination=p_Destination,
     Taskid=p_TaskID,
     Infotype=p_InfoType,
     Linecount=p_LineCount,
     Format1=p_Format1,
     Format2=p_Format2,
     datasection=p_DataSection,
--     Fileindex,
     Reserve=p_Reserve;
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
prompt Creating procedure UP_GZJH_UPDATEFILEINDEX
prompt ==========================================
prompt
create or replace procedure htcuser.up_gzjh_updatefileindex
(
       v_Id  tb_gzjh.Id%type,
       p_FileIndex tb_gzjh.fileindex%type,
       v_result out number
)
is
begin
     update   tb_gzjh t set
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
prompt Creating procedure UP_JH_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.UP_JH_GETLIST
(
       p_planType IN TB_JH.Plantype%type Default null,
       p_planAging IN Tb_Jh.Id%type Default null,
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
prompt Creating procedure UP_TYSJ_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_TYSJ_insert
(
       p_CTime tb_TYSJ.Ctime%type,
       p_Source tb_TYSJ.Source%type,
       p_Destination tb_TYSJ.Destination%type,
       p_TaskID tb_TYSJ.Taskid%type,
       p_InfoType tb_TYSJ.Infotype%type,
       p_LineCount tb_TYSJ.Linecount%type,
       p_Format1 tb_TYSJ.format1%type,
       p_DataSection tb_TYSJ.datasection%type,
       p_FileIndex tb_TYSJ.Fileindex%type,
       p_Reserve tb_TYSJ.Reserve%type,
       v_Id out tb_TYSJ.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0045'));
       insert into tb_TYSJ(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Format1,datasection,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_Format1,p_DataSection,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_TYSJ_insert;
/

prompt
prompt Creating procedure UP_TYSJ_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.up_TYSJ_selectbyid
(
       p_Id tb_tysj.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_tysj where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_TYSJ_UPDATE
prompt =================================
prompt
create or replace procedure htcuser.up_TYSJ_update
(
       v_Id out tb_TYSJ.Id%type,
       p_Source tb_TYSJ.Source%type,
       p_Destination tb_TYSJ.Destination%type,
       p_TaskID tb_TYSJ.Taskid%type,
       p_InfoType tb_TYSJ.Infotype%type,
       p_LineCount tb_TYSJ.Linecount%type,
       p_Format1 tb_TYSJ.format1%type,
       p_DataSection tb_TYSJ.datasection%type,
       p_Reserve tb_TYSJ.Reserve%type,
       v_result out number
)
is
begin
     update   tb_TYSJ set
     Source=p_Source,
     Destination=p_Destination,
     Taskid=p_TaskID,
     Infotype=p_InfoType,
     Linecount=p_LineCount,
     Format1=p_Format1,
     datasection=p_DataSection,
--     Fileindex,
     Reserve=p_Reserve;
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
prompt Creating procedure UP_TYSJ_UPDATEFILEINDEX
prompt ==========================================
prompt
create or replace procedure htcuser.up_tysj_updatefileindex
(
       v_Id  tb_tysj.Id%type,
       p_FileIndex tb_tysj.fileindex%type,
       v_result out number
)
is
begin
     update   tb_tysj t set
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
prompt Creating procedure UP_XXXQ_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_xxxq_insert
(
       p_CTime tb_xxxq.Ctime%type,
       p_Source tb_xxxq.Source%type,
       p_Destination tb_xxxq.Destination%type,
       p_TaskID tb_xxxq.Taskid%type,
       p_InfoType tb_xxxq.Infotype%type,
       p_LineCount tb_xxxq.Linecount%type,
       p_Format1 tb_xxxq.format1%type,
       p_Format2 tb_xxxq.format2%type,
       p_DataSection tb_xxxq.datasection%type,
       p_FileIndex tb_xxxq.Fileindex%type,
       p_Reserve tb_xxxq.Reserve%type,
       v_Id out tb_xxxq.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0042'));
       insert into tb_xxxq(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Format1,Format2,datasection,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_Format1,p_Format1,p_DataSection,p_FileIndex,p_Reserve);
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
prompt Creating procedure UP_XXXQ_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.up_XXXQ_selectbyid
(
       p_Id tb_xxxq.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_xxxq where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_XXXQ_UPDATE
prompt =================================
prompt
create or replace procedure htcuser.up_xxxq_update
(
       v_Id out tb_xxxq.Id%type,
       p_Source tb_xxxq.Source%type,
       p_Destination tb_xxxq.Destination%type,
       p_TaskID tb_xxxq.Taskid%type,
       p_InfoType tb_xxxq.Infotype%type,
       p_LineCount tb_xxxq.Linecount%type,
       p_Format1 tb_xxxq.format1%type,
       p_Format2 tb_xxxq.format2%type,
       p_DataSection tb_xxxq.datasection%type,
       p_Reserve tb_xxxq.Reserve%type,
       v_result out number
)
is
begin
     update   tb_xxxq set
     Source=p_Source,
     Destination=p_Destination,
     Taskid=p_TaskID,
     Infotype=p_InfoType,
     Linecount=p_LineCount,
     Format1=p_Format1,
     Format2=p_Format2,
     datasection=p_DataSection,
--     Fileindex,
     Reserve=p_Reserve;
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
prompt Creating procedure UP_XXXQ_UPDATEFILEINDEX
prompt ==========================================
prompt
create or replace procedure htcuser.up_xxxq_updatefileindex
(
       v_Id  tb_xxxq.Id%type,
       p_FileIndex tb_xxxq.fileindex%type,
       v_result out number
)
is
begin
     update   tb_xxxq t set
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
            select * from v_YDSJ where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_YJJH_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_yjjh_insert
(
       p_CTime tb_yjjh.Ctime%type,
       p_Source tb_yjjh.Source%type,
       p_Destination tb_yjjh.Destination%type,
       p_TaskID tb_yjjh.Taskid%type,
       p_InfoType tb_yjjh.Infotype%type,
       p_LineCount tb_yjjh.Linecount%type,
       p_Format1 tb_yjjh.format1%type,
       p_DataSection tb_yjjh.datasection%type,
       p_FileIndex tb_yjjh.Fileindex%type,
       p_Reserve tb_yjjh.Reserve%type,
       v_Id out tb_yjjh.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0041'));
       insert into tb_yjjh(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Format1,datasection,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_Format1,p_DataSection,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_yjjh_insert;
/

prompt
prompt Creating procedure UP_YJJH_SELECTBYID
prompt =====================================
prompt
create or replace procedure htcuser.up_yjjh_selectbyid
(
       p_Id tb_yjjh.id%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_yjjh where id=p_Id;
end;
/

prompt
prompt Creating procedure UP_YJJH_UPDATE
prompt =================================
prompt
create or replace procedure htcuser.up_yjjh_update
(
       v_Id  tb_yjjh.Id%type,
       p_Source tb_yjjh.Source%type,
       p_Destination tb_yjjh.Destination%type,
       p_TaskID tb_yjjh.Taskid%type,
       p_InfoType tb_yjjh.Infotype%type,
       p_LineCount tb_yjjh.Linecount%type,
       p_Format1 tb_yjjh.format1%type,
       p_DataSection tb_yjjh.datasection%type,
       p_Reserve tb_yjjh.Reserve%type,
       v_result out number
)
is
begin
     update   tb_yjjh t set
     t.Source=p_Source,
     t.Destination=p_Destination,
     t.Taskid=p_TaskID,
     t.Infotype=p_InfoType,
     t.Linecount=p_LineCount,
     t.Format1=p_Format1,
     t.datasection=p_DataSection,
     t.Reserve=p_Reserve  where t.Id = v_Id;
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
prompt Creating procedure UP_YJJH_UPDATEFILEINDEX
prompt ==========================================
prompt
create or replace procedure htcuser.up_yjjh_updatefileindex
(
       v_Id  tb_yjjh.Id%type,
       p_FileIndex tb_yjjh.fileindex%type,
       v_result out number
)
is
begin
     update   tb_yjjh t set
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


spool off

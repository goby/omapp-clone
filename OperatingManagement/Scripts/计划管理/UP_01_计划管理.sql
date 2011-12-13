---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2011/12/13, 0:01:01 --
---------------------------------------------

spool UP_01_计划管理.log

prompt
prompt Creating procedure UP_AE_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.up_ae_Getlist
(
       p_startTime in TB_ae.Ctime%type,
       p_endTime in TB_ae.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,          
          t.CTIME,        
          t.RESERVE,      
          t.DATA_ZT,      
          t.DATA_T,       
          t.DATA_A,       
          t.DATA_E,       
          t.DATA_DELTAA1, 
          t.DATA_DELTAE1, 
          t.DATA_DELTAA2, 
          t.DATA_DELTAE2, 
          t.DATA_SPHI,    
          t.DATA_ID
  FROM  TB_ae t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_ae_Getlist;
/

prompt
prompt Creating procedure UP_AE_INSERT
prompt ===============================
prompt
create or replace procedure htcuser.up_ae_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_ae.Reserve%type,
       p_Data_ZT tb_ae.data_zt%type,
       p_Data_T tb_ae.data_t%type,
       p_Data_A tb_ae.data_a%type,
       p_Data_E tb_ae.data_e%type,
       p_Data_DeltaA1 tb_ae.data_deltaa1%type,
       p_Data_DeltaE1 tb_ae.data_deltae1%type,
       p_Data_DeltaA2 tb_ae.data_deltaa2%type,
       p_Data_DeltaE2 tb_ae.data_deltae2%type,
       p_Data_SPHI tb_ae.data_sphi%type,
       v_Id out tb_ae.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0057'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_ae (ID, CTIME, RESERVE, DATA_ZT, DATA_T, DATA_A, DATA_E, DATA_DELTAA1, DATA_DELTAE1, DATA_DELTAA2, DATA_DELTAE2, DATA_SPHI, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_ZT,p_Data_T,p_Data_A,p_Data_E,p_Data_DeltaA1,p_Data_DeltaE1,p_Data_DeltaA2,p_Data_DeltaE2,p_Data_SPHI,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_ae_insert;
/

prompt
prompt Creating procedure UP_CZJG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_CZJG_insert
(
       p_CTime tb_CZJG.Ctime%type,
       p_Source tb_CZJG.Source%type,
       p_Destination tb_CZJG.Destination%type,
       p_TaskID tb_CZJG.Taskid%type,
       p_InfoType tb_CZJG.Infotype%type,
       p_LineCount tb_CZJG.Linecount%type,
       p_FileIndex tb_CZJG.Fileindex%type,
       p_Reserve tb_CZJG.Reserve%type,
       v_Id out tb_CZJG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0018'));
       insert into tb_CZJG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_CZJG_insert;
/

prompt
prompt Creating procedure UP_CZZT_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_czzt_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_czzt.Reserve%type,
       p_Data_BJ tb_czzt.data_bj%type,
       p_Data_ZX tb_czzt.data_zx%type,
       p_Data_T0 tb_czzt.data_t0%type,
       p_Data_T1 tb_czzt.data_t1%type,
       p_Data_A tb_czzt.data_a%type,
       p_Data_E tb_czzt.data_e%type,
       p_Data_I tb_czzt.data_i%type,
       p_Data_OHM tb_czzt.data_ohm%type,
       p_Data_OMEGA tb_czzt.data_omega%type,
       v_Id out tb_czzt.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0058'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_czzt (ID, CTIME, RESERVE, DATA_BJ, DATA_ZX, DATA_T0, DATA_T1, DATA_A, DATA_E, DATA_I, DATA_OHM, DATA_OMEGA, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_BJ,p_Data_ZX,p_Data_T0,p_Data_T1,p_Data_A,p_Data_E,p_Data_I,p_Data_OHM,p_Data_OMEGA,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_czzt_insert;
/

prompt
prompt Creating procedure UP_GCJG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_GCJG_insert
(
       p_CTime tb_GCJG.Ctime%type,
       p_Source tb_GCJG.Source%type,
       p_Destination tb_GCJG.Destination%type,
       p_TaskID tb_GCJG.Taskid%type,
       p_InfoType tb_GCJG.Infotype%type,
       p_LineCount tb_GCJG.Linecount%type,
       p_FileIndex tb_GCJG.Fileindex%type,
       p_Reserve tb_GCJG.Reserve%type,
       v_Id out tb_GCJG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0017'));
       insert into tb_GCJG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_GCJG_insert;
/

prompt
prompt Creating procedure UP_GDSH_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.up_gdsh_Getlist
(
       p_startTime in TB_GDSH.Ctime%type,
       p_endTime in TB_GDSH.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,       
          t.CTIME,    
          t.RESERVE,   
          t.DATA_D,   
          t.DATA_T,    
          t.DATA_A,    
          t.DATA_E,    
          t.DATA_I,    
          t.DATA_OHM,  
          t.DATA_OMEGA,
          t.DATA_M,    
          t.DATA_CDSM, 
          t.DATA_KSM,  
          t.DATA_KZ1,  
          t.DATA_KZ2,  
          t.DATA_ID 
  FROM  TB_GDSH t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_gdsh_Getlist;
/

prompt
prompt Creating procedure UP_GDSH_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_GDSH_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_gdsh.Reserve%type,
       p_Data_D tb_gdsh.Data_d%type,
       p_Data_T tb_gdsh.Data_t%type,
       p_Data_A tb_gdsh.Data_a%type,
       p_Data_E tb_gdsh.Data_e%type,
       p_Data_I tb_gdsh.Data_i%type,
       p_Data_Ohm tb_gdsh.Data_Ohm%type,
       p_Data_Omega tb_gdsh.Data_Omega%type,
       p_Data_M tb_gdsh.Data_m%type,
       p_Data_CDSM tb_gdsh.data_cdsm%type,
       p_Data_KSM tb_gdsh.data_ksm%type,
       p_Data_KZ1 tb_gdsh.data_kz1%type,
       p_Data_KZ2 tb_gdsh.data_kz2%type,
       v_Id out tb_gdsh.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0052'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_gdsh (ID, CTIME, RESERVE, DATA_D, DATA_T, DATA_A, DATA_E, DATA_I, DATA_OHM, DATA_OMEGA, DATA_M, DATA_CDSM, DATA_KSM, DATA_KZ1, DATA_KZ2, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_D,p_Data_T,p_Data_A,p_Data_E,p_Data_I,p_Data_Ohm,p_Data_Omega,p_Data_M,p_Data_CDSM,p_Data_KSM,p_Data_KZ1,p_Data_KZ2,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_GDSH_insert;
/

prompt
prompt Creating procedure UP_GDXA_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.up_GDXA_Getlist
(
       p_startTime in TB_GDXA.Ctime%type,
       p_endTime in TB_GDXA.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,
          t.CTIME,
          t.SOURCE,
          t.DESTINATION,
          t.TASKID,
          t.INFOTYPE,
          t.LINECOUNT,
          t.FILEINDEX,
          t.RESERVE
  FROM  TB_GDXA t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_GDXA_Getlist;
/

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
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
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
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0051'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);
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
            select * from v_GD where id=p_Id;
end;
/

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
prompt Creating procedure UP_HJXX_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_HJXX_insert
(
       p_CTime tb_HJXX.Ctime%type,
       p_Source tb_HJXX.Source%type,
       p_Destination tb_HJXX.Destination%type,
       p_TaskID tb_HJXX.Taskid%type,
       p_InfoType tb_HJXX.Infotype%type,
       p_LineCount tb_HJXX.Linecount%type,
       p_FileIndex tb_HJXX.Fileindex%type,
       p_Reserve tb_HJXX.Reserve%type,
       v_Id out tb_HJXX.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0013'));
       insert into tb_HJXX(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_HJXX_insert;
/

prompt
prompt Creating procedure UP_JDJG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_JDJG_insert
(
       p_CTime tb_JDJG.Ctime%type,
       p_Source tb_JDJG.Source%type,
       p_Destination tb_JDJG.Destination%type,
       p_TaskID tb_JDJG.Taskid%type,
       p_InfoType tb_JDJG.Infotype%type,
       p_LineCount tb_JDJG.Linecount%type,
       p_FileIndex tb_JDJG.Fileindex%type,
       p_Reserve tb_JDJG.Reserve%type,
       v_Id out tb_JDJG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0019'));
       insert into tb_JDJG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_JDJG_insert;
/

prompt
prompt Creating procedure UP_JHBG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_JHBG_insert
(
       p_CTime tb_JHBG.Ctime%type,
       p_Source tb_JHBG.Source%type,
       p_Destination tb_JHBG.Destination%type,
       p_TaskID tb_JHBG.Taskid%type,
       p_InfoType tb_JHBG.Infotype%type,
       p_LineCount tb_JHBG.Linecount%type,
       p_FileIndex tb_JHBG.Fileindex%type,
       p_Reserve tb_JHBG.Reserve%type,
       v_Id out tb_JHBG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0015'));
       insert into tb_JHBG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_JHBG_insert;
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
prompt Creating procedure up_jh_update
prompt =================================
prompt
create or replace procedure up_jh_update
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
     t.endtime = p_endtime,
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
prompt Creating procedure UP_MBXX_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_MBXX_insert
(
       p_CTime tb_MBXX.Ctime%type,
       p_Source tb_MBXX.Source%type,
       p_Destination tb_MBXX.Destination%type,
       p_TaskID tb_MBXX.Taskid%type,
       p_InfoType tb_MBXX.Infotype%type,
       p_LineCount tb_MBXX.Linecount%type,
       p_FileIndex tb_MBXX.Fileindex%type,
       p_Reserve tb_MBXX.Reserve%type,
       v_Id out tb_MBXX.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0012'));
       insert into tb_MBXX(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_MBXX_insert;
/

prompt
prompt Creating procedure UP_RR_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.up_rr_Getlist
(
       p_startTime in TB_rr.Ctime%type,
       p_endTime in TB_rr.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,         
          t.CTIME,      
          t.RESERVE,    
          t.DATA_ZT,    
          t.DATA_T,     
          t.DATA_RR,    
          t.DATA_DELTAF,
          t.DATA_SPHI,  
          t.DATA_ID         
  FROM  TB_rr t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_rr_Getlist;
/

prompt
prompt Creating procedure UP_RR_INSERT
prompt ===============================
prompt
create or replace procedure htcuser.up_rr_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_rr.Reserve%type,
       p_Data_ZT tb_rr.data_zt%type,
       p_Data_T tb_rr.data_t%type,
       p_Data_RR tb_rr.data_rr%type,
       p_Data_DeltaF tb_rr.data_deltaf%type,
       p_Data_SPHI tb_rr.data_sphi%type,
       v_Id out tb_rr.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0056'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_rr (ID, CTIME, RESERVE, DATA_ZT, DATA_T, DATA_RR, DATA_DELTAF, DATA_SPHI, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_ZT,p_Data_T,p_Data_RR,p_Data_DeltaF,p_Data_SPHI,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_rr_insert;
/

prompt
prompt Creating procedure UP_R_GETLIST
prompt ===============================
prompt
create or replace procedure htcuser.up_r_Getlist
(
       p_startTime in TB_r.Ctime%type,
       p_endTime in TB_r.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,     
          t.CTIME,      
          t.RESERVE,    
          t.DATA_ZT,
          t.DATA_T,     
          t.DATA_RE,    
          t.DATA_DELTAT,
          t.DATA_SPHI,  
          t.DATA_ID          
  FROM  TB_r t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_r_Getlist;
/

prompt
prompt Creating procedure UP_R_INSERT
prompt ==============================
prompt
create or replace procedure htcuser.up_r_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_r.Reserve%type,
       p_Data_ZT tb_r.data_zt%type,
       p_Data_T tb_r.data_t%type,
       p_Data_RE tb_r.data_re%type,
       p_Data_DeltaT tb_r.data_deltat%type,
       p_Data_SPHI tb_r.data_sphi%type,
       v_Id out tb_r.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0055'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_r (ID, CTIME, RESERVE, DATA_ZT, DATA_T, DATA_RE, DATA_DELTAT, DATA_SPHI, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_ZT,p_Data_T,p_Data_RE,p_Data_DeltaT,p_Data_SPHI,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_r_insert;
/

prompt
prompt Creating procedure UP_SBJH_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_SBJH_insert
(
	     p_CTime tb_SBJH.Ctime%type,
       p_Source tb_SBJH.Source%type,
       p_Destination tb_SBJH.Destination%type,
       p_TaskID tb_SBJH.Taskid%type,
       p_InfoType tb_SBJH.Infotype%type,
       p_LineCount tb_SBJH.Linecount%type,
       p_FileIndex tb_SBJH.Fileindex%type,
       p_Reserve tb_SBJH.Reserve%type,
       v_Id out tb_SBJH.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0011'));
       insert into tb_SBJH(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_SBJH_insert;
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
prompt Creating procedure UP_SYXQ_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_SYXQ_insert
(
       p_CTime tb_SYXQ.Ctime%type,
       p_Source tb_SYXQ.Source%type,
       p_Destination tb_SYXQ.Destination%type,
       p_TaskID tb_SYXQ.Taskid%type,
       p_InfoType tb_SYXQ.Infotype%type,
       p_LineCount tb_SYXQ.Linecount%type,
       p_FileIndex tb_SYXQ.Fileindex%type,
       p_Reserve tb_SYXQ.Reserve%type,
       v_Id out tb_SYXQ.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0016'));
       insert into tb_SYXQ(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_SYXQ_insert;
/

prompt
prompt Creating procedure UP_T0_GETLIST
prompt ================================
prompt
create or replace procedure htcuser.up_T0_Getlist
(
       p_startTime in TB_T0.Ctime%type,
       p_endTime in TB_T0.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,
          t.CTIME,
          t.RESERVE,
          t.data_tzero,
          t.data_id
  FROM  TB_T0 t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_T0_Getlist;
/

prompt
prompt Creating procedure UP_T0_INSERT
prompt ===============================
prompt
create or replace procedure htcuser.up_t0_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_t0.Reserve%type,
       p_Data_TZero tb_t0.data_tzero%type,
       v_Id out tb_t0.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0054'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_t0 (ID, CTIME, RESERVE, DATA_TZero, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_TZero,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_t0_insert;
/

prompt
prompt Creating procedure UP_TYJG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_TYJG_insert
(
       p_CTime tb_TYJG.Ctime%type,
       p_Source tb_TYJG.Source%type,
       p_Destination tb_TYJG.Destination%type,
       p_TaskID tb_TYJG.Taskid%type,
       p_InfoType tb_TYJG.Infotype%type,
       p_LineCount tb_TYJG.Linecount%type,
       p_FileIndex tb_TYJG.Fileindex%type,
       p_Reserve tb_TYJG.Reserve%type,
       v_Id out tb_TYJG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0020'));
       insert into tb_TYJG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_TYJG_insert;
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
prompt Creating procedure UP_XDSC_GETLIST
prompt ==================================
prompt
create or replace procedure htcuser.up_XDSC_Getlist
(
       p_startTime in TB_XDSC.Ctime%type,
       p_endTime in TB_XDSC.Ctime%type,
       o_cursor out sys_refcursor
) is
begin
   OPEN o_cursor FOR
  SELECT
          t.ID,        
          t.CTIME,      
          t.RESERVE,    
          t.DATA_D,     
          t.DATA_T,     
          t.DATA_N,     
          t.DATA_DELTAT,
          t.DATA_ID    
  FROM  TB_XDSC t
  WHERE
  t.ctime >= p_startTime
  AND t.ctime <= p_endTime


;
end up_XDSC_Getlist;
/

prompt
prompt Creating procedure UP_XDSC_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_xdsc_insert
(
       p_Version  TB_UDPDATABASICINFO.Version%type,
       p_Flag  TB_UDPDATABASICINFO.Flag%type,
       p_Maintype  TB_UDPDATABASICINFO.Maintype%type,
       p_datatype  TB_UDPDATABASICINFO.Datatype%type,
       p_Source  TB_UDPDATABASICINFO.Source%type,
       p_Destination  TB_UDPDATABASICINFO.Destination%type,
       p_missioncode  TB_UDPDATABASICINFO.Missioncode%type,
       p_satellitecode  TB_UDPDATABASICINFO.Satellitecode%type,
       p_datadate  TB_UDPDATABASICINFO.Datadate%type,
       p_datatime  TB_UDPDATABASICINFO.Datatime%type,
       p_sequencenumber  TB_UDPDATABASICINFO.Sequencenumber%type,
       p_childrenpacknumber  TB_UDPDATABASICINFO.Childrenpacknumber%type,
       p_udpReserve  TB_UDPDATABASICINFO.Reserve%type,
       p_datalength  TB_UDPDATABASICINFO.Datalength%type,
       p_dataclass  TB_UDPDATABASICINFO.Dataclass%type,
       p_Reserve tb_xdsc.Reserve%type,
       p_Data_D tb_xdsc.Data_d%type,
       p_Data_T tb_xdsc.Data_t%type,
       p_Data_N tb_xdsc.data_n%type,
       p_Data_DeltaT tb_xdsc.data_deltat%type,
       v_Id out tb_xdsc.Id%type,
       v_result out number
)
is
       m_UDPId integer;
begin
       m_UDPId := to_number(fn_genseqnum('0050'));
       v_Id:= to_number(fn_genseqnum('0053'));
       insert into tb_udpdatabasicinfo (ID, CTIME, VERSION, FLAG, MAINTYPE, DATATYPE, SOURCE, DESTINATION, MISSIONCODE, SATELLITECODE, DATADATE, DATATIME, SEQUENCENUMBER, CHILDRENPACKNUMBER, RESERVE, DATALENGTH, DATACLASS)
       values(m_UDPId,sysdate(),p_Version,p_Flag,p_Maintype,p_datatype,p_Source,p_Destination,p_missioncode,p_satellitecode,p_datadate,p_datatime,p_sequencenumber,p_childrenpacknumber,p_udpReserve,p_datalength,p_dataclass);

       insert into tb_xdsc (ID, CTIME, RESERVE, DATA_D, DATA_T, DATA_N, DATA_DELTAT, DATA_ID)
       values(v_Id,sysdate(),p_Reserve,p_Data_D,p_Data_T,p_Data_N,p_Data_DeltaT,m_UDPId);

       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_xdsc_insert;
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
prompt Creating procedure UP_YJBG_INSERT
prompt =================================
prompt
create or replace procedure htcuser.up_YJBG_insert
(
       p_CTime tb_YJBG.Ctime%type,
       p_Source tb_YJBG.Source%type,
       p_Destination tb_YJBG.Destination%type,
       p_TaskID tb_YJBG.Taskid%type,
       p_InfoType tb_YJBG.Infotype%type,
       p_LineCount tb_YJBG.Linecount%type,
       p_FileIndex tb_YJBG.Fileindex%type,
       p_Reserve tb_YJBG.Reserve%type,
       v_Id out tb_YJBG.Id%type,
       v_result out number
)
is
begin
       v_Id:= to_number(fn_genseqnum('0014'));
       insert into tb_YJBG(id,Ctime,Source,Destination,Taskid,Infotype,Linecount,Fileindex,Reserve)
       values(v_Id,sysdate(),p_Source,p_Destination,p_TaskID,p_InfoType,p_LineCount,p_FileIndex,p_Reserve);
       commit;
       v_result:=5; -- Success

       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;
          COMMIT;
          v_result:=4; --Error
end up_YJBG_insert;
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

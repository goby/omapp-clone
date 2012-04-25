prompt PL/SQL Developer import file
prompt Created on 2012��4��25�� by Cindy
set feedback off
set define off
prompt Dropping TB_XYXSINFO...
drop table TB_XYXSINFO cascade constraints;
prompt Creating TB_XYXSINFO...
create table TB_XYXSINFO
(
  rid      NUMBER(5) not null,
  addrname VARCHAR2(100) not null,
  addrmark VARCHAR2(10),
  incode   VARCHAR2(10) not null,
  excode   VARCHAR2(20) not null,
  mainip   VARCHAR2(15),
  tcpport  NUMBER(8),
  bakip    VARCHAR2(15),
  udpport  NUMBER(8),
  ftppath  VARCHAR2(50)
)
tablespace TSHTC
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
comment on column TB_XYXSINFO.rid
  is '��¼��';
comment on column TB_XYXSINFO.addrname
  is '��ַ����';
comment on column TB_XYXSINFO.addrmark
  is '��ַ��ʶ';
comment on column TB_XYXSINFO.incode
  is '�ڲ�ʮ�����Ʊ���';
comment on column TB_XYXSINFO.excode
  is '�ⲿʮ�����Ʊ���';
comment on column TB_XYXSINFO.mainip
  is '��IP��ַ';
comment on column TB_XYXSINFO.tcpport
  is 'TCP�˿�';
comment on column TB_XYXSINFO.bakip
  is '����IP��ַ';
comment on column TB_XYXSINFO.udpport
  is 'UDP�˿�';
comment on column TB_XYXSINFO.ftppath
  is 'FTP·��@�û���@����';

prompt Disabling triggers for TB_XYXSINFO...
alter table TB_XYXSINFO disable all triggers;
prompt 23 records loaded
prompt Enabling triggers for TB_XYXSINFO...
alter table TB_XYXSINFO enable all triggers;
set feedback on
set define on
prompt Done.

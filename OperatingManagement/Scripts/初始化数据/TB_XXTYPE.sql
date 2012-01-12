prompt PL/SQL Developer import file
prompt Created on 2011��12��31�� by Cindy
set feedback off
set define off
prompt Creating TB_XXTYPE...
create table TB_XXTYPE
(
  rid      NUMBER(5) not null,
  dataname VARCHAR2(50) not null,
  inmark   VARCHAR2(10) not null,
  exmark   VARCHAR2(10) not null,
  incode   VARCHAR2(10),
  excode   VARCHAR2(20),
  datatype NUMBER(1) not null
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
comment on column TB_XXTYPE.rid
  is '��¼��';
comment on column TB_XXTYPE.dataname
  is '��Ϣ����';
comment on column TB_XXTYPE.inmark
  is '�ڲ���ʶ';
comment on column TB_XXTYPE.exmark
  is '�ⲿ��ʶ';
comment on column TB_XXTYPE.incode
  is '�ڲ�����';
comment on column TB_XXTYPE.excode
  is '�ⲿ����';
comment on column TB_XXTYPE.datatype
  is '��Ϣ���ͣ�0-����֡��1-�ļ�';
alter table TB_XXTYPE
  add constraint PK_TB_XXTYPE primary key (RID)
  using index 
  tablespace TSHTC
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );

prompt Disabling triggers for TB_XXTYPE...
alter table TB_XXTYPE disable all triggers;
prompt Deleting TB_XXTYPE...
delete from TB_XXTYPE;
commit;
prompt Loading TB_XXTYPE...

prompt 54 records loaded
prompt Enabling triggers for TB_XXTYPE...
alter table TB_XXTYPE enable all triggers;
set feedback on
set define on
prompt Done.

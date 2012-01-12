prompt PL/SQL Developer import file
prompt Created on 2011年12月19日 by Cindy
set feedback off
set define off
prompt Creating TB_XYXSINFO...
create table TB_XYXSINFO
(
  rid        NUMBER(5) not null,
  "ADDRName" VARCHAR2(100) not null,
  addrmark   VARCHAR2(10),
  incode     VARCHAR2(10) not null,
  excode     VARCHAR2(20) not null
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
  is '记录号';
comment on column TB_XYXSINFO."ADDRName"
  is '地址名称';
comment on column TB_XYXSINFO.addrmark
  is '地址标识';
comment on column TB_XYXSINFO.incode
  is '内部十六进制编码';
comment on column TB_XYXSINFO.excode
  is '外部十六进制编码';
alter table TB_XYXSINFO
  add constraint PK_TB_XYXSINFO primary key (RID)
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

prompt Disabling triggers for TB_XYXSINFO...
alter table TB_XYXSINFO disable all triggers;
prompt Deleting TB_XYXSINFO...
delete from TB_XYXSINFO;
commit;
prompt Loading TB_XYXSINFO...

prompt 23 records loaded
prompt Enabling triggers for TB_XYXSINFO...
alter table TB_XYXSINFO enable all triggers;
set feedback on
set define on
prompt Done.

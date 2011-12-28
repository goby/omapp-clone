-- Create table
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
    initial 64
    minextents 1
    maxextents unlimited
  );
-- Add comments to the columns 
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
-- Create/Recreate primary, unique and foreign key constraints 
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

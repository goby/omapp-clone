-- Create table
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
    initial 64
    minextents 1
    maxextents unlimited
  );
-- Add comments to the columns 
comment on column TB_XXTYPE.rid
  is '记录号';
comment on column TB_XXTYPE.dataname
  is '信息名称';
comment on column TB_XXTYPE.inmark
  is '内部标识';
comment on column TB_XXTYPE.exmark
  is '外部标识';
comment on column TB_XXTYPE.incode
  is '内部编码';
comment on column TB_XXTYPE.excode
  is '外部编码';
comment on column TB_XXTYPE.datatype
  is '信息类型：0-数据帧；1-文件';
-- Create/Recreate primary, unique and foreign key constraints 
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

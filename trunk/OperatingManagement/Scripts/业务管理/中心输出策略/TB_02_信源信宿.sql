--本地测试需要执行此脚本，上线时甲方DB中应该存在该表及对应数据
-- Create table
create table TB_XYXSINFO
(
  rid      NUMBER(5) not null,
  addrname VARCHAR2(100) not null,
  addrmark VARCHAR2(10),
  incode   VARCHAR2(10) not null,
  excode   VARCHAR2(20) not null,
  mainip   VARCHAR2(15),
  mainport NUMBER(8),
  bakip    VARCHAR2(15),
  bakport  NUMBER(8)
)
tablespace TSHTC
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 16
    minextents 1
    maxextents unlimited
  );
-- Add comments to the columns 
comment on column TB_XYXSINFO.rid
  is '记录号';
comment on column TB_XYXSINFO.addrname
  is '地址名称';
comment on column TB_XYXSINFO.addrmark
  is '地址标识';
comment on column TB_XYXSINFO.incode
  is '内部十六进制编码';
comment on column TB_XYXSINFO.excode
  is '外部十六进制编码';
comment on column TB_XYXSINFO.mainip
  is '主IP地址';
comment on column TB_XYXSINFO.mainport
  is '主端口';
comment on column TB_XYXSINFO.bakip
  is '备用IP地址';
comment on column TB_XYXSINFO.bakport
  is '备用端口';
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

-- Create table
create table TB_CENTEROUTPUTPOLICY
(
  copid         NUMBER(10) not null,
  infofrom      VARCHAR2(4) not null,
  infotype      VARCHAR2(4) not null,
  infoto        VARCHAR2(4) not null,
  note          NVARCHAR2(200),
  createdtime   DATE not null,
  createduserid NUMBER(10),
  updatedtime   DATE,
  updateduserid NUMBER(10)
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
-- Add comments to the columns 
comment on column TB_CENTEROUTPUTPOLICY.copid
  is '序号';
comment on column TB_CENTEROUTPUTPOLICY.infofrom
  is '信源';
comment on column TB_CENTEROUTPUTPOLICY.infotype
  is '信息类别';
comment on column TB_CENTEROUTPUTPOLICY.infoto
  is '信宿';
comment on column TB_CENTEROUTPUTPOLICY.note
  is '描述';
comment on column TB_CENTEROUTPUTPOLICY.createdtime
  is '创建时间';
comment on column TB_CENTEROUTPUTPOLICY.createduserid
  is '创建用户ID';
comment on column TB_CENTEROUTPUTPOLICY.updatedtime
  is '最后修改时间';
comment on column TB_CENTEROUTPUTPOLICY.updateduserid
  is '最后修改用户ID';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_CENTEROUTPUTPOLICY
  add constraint PK_TB_CENTEROUTPUTPOLICY primary key (COPID)
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

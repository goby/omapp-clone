-- Create table
create table TB_USESTATUS
(
  usid          NUMBER(10) not null,
  resourceid    NUMBER(10) not null,
  resourcetype  NUMBER(2) not null,
  usedtype      NUMBER(2) not null,
  begintime     DATE not null,
  endtime       DATE not null,
  usedby        NVARCHAR2(50),
  usedcategory  NVARCHAR2(50),
  usedfor       NVARCHAR2(100),
  canbeused     NUMBER(2),
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
    initial 16K
    minextents 1
    maxextents unlimited
  );
-- Add comments to the columns 
comment on column TB_USESTATUS.usid
  is '���';
comment on column TB_USESTATUS.resourceid
  is '��Դ���';
comment on column TB_USESTATUS.resourcetype
  is '��Դ����';
comment on column TB_USESTATUS.usedtype
  is 'ռ������';
comment on column TB_USESTATUS.begintime
  is '��ʼʱ��';
comment on column TB_USESTATUS.endtime
  is '����ʱ��';
comment on column TB_USESTATUS.usedby
  is '�������';
comment on column TB_USESTATUS.usedcategory
  is '��������';
comment on column TB_USESTATUS.usedfor
  is 'ռ��ԭ��';
comment on column TB_USESTATUS.canbeused
  is '�Ƿ��ִ������';
comment on column TB_USESTATUS.createdtime
  is '����ʱ��';
comment on column TB_USESTATUS.createduserid
  is '�����û�';
comment on column TB_USESTATUS.updatedtime
  is '����޸�ʱ��';
comment on column TB_USESTATUS.updateduserid
  is '�޸��û�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_USESTATUS
  add constraint PK_TB_USESTATUS primary key (USID)
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

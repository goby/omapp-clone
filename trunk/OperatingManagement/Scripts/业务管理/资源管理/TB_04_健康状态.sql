-- Create table
create table TB_HEALTHSTATUS
(
  hsid          NUMBER(10) not null,
  resourceid    NUMBER(10) not null,
  resourcetype  NUMBER(2) not null,
  functiontype  NVARCHAR2(50),
  status        NUMBER(2) not null,
  begintime     DATE,
  endtime       DATE,
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
comment on column TB_HEALTHSTATUS.hsid
  is '���';
comment on column TB_HEALTHSTATUS.resourceid
  is '��Դ���';
comment on column TB_HEALTHSTATUS.resourcetype
  is '��Դ����';
comment on column TB_HEALTHSTATUS.functiontype
  is '��������';
comment on column TB_HEALTHSTATUS.status
  is '����״̬';
comment on column TB_HEALTHSTATUS.begintime
  is '��ʼʱ��';
comment on column TB_HEALTHSTATUS.endtime
  is '����ʱ��';
comment on column TB_HEALTHSTATUS.createdtime
  is '����ʱ��';
comment on column TB_HEALTHSTATUS.createduserid
  is '�����û�ID';
comment on column TB_HEALTHSTATUS.updatedtime
  is '����޸�ʱ��';
comment on column TB_HEALTHSTATUS.updateduserid
  is '�޸��û�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_HEALTHSTATUS
  add constraint PK_TB_HEALTHSTATUS primary key (HSID)
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

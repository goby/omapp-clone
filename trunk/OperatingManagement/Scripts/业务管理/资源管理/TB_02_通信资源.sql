-- Create table
create table TB_COMMUNICATIONRESOURCE
(
  crid          NUMBER(10) not null,
  routename     NVARCHAR2(50) not null,
  routecode     NVARCHAR2(50) not null,
  direction     NVARCHAR2(20) not null,
  bandwidth     NUMBER(12,2) not null,
  status        NUMBER(2) not null,
  extproperties NVARCHAR2(2000),
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
comment on column TB_COMMUNICATIONRESOURCE.crid
  is '���';
comment on column TB_COMMUNICATIONRESOURCE.routename
  is '��·����';
comment on column TB_COMMUNICATIONRESOURCE.routecode
  is '��·���';
comment on column TB_COMMUNICATIONRESOURCE.direction
  is '����';
comment on column TB_COMMUNICATIONRESOURCE.bandwidth
  is '����';
comment on column TB_COMMUNICATIONRESOURCE.status
  is '״̬';
comment on column TB_COMMUNICATIONRESOURCE.extproperties
  is '��չ����';
comment on column TB_COMMUNICATIONRESOURCE.createdtime
  is '����ʱ��';
comment on column TB_COMMUNICATIONRESOURCE.createduserid
  is '�����û�';
comment on column TB_COMMUNICATIONRESOURCE.updatedtime
  is '����޸�ʱ��';
comment on column TB_COMMUNICATIONRESOURCE.updateduserid
  is '�޸��û�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_COMMUNICATIONRESOURCE
  add constraint PK_TB_COMMUNICATIONRESOURCE primary key (CRID)
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

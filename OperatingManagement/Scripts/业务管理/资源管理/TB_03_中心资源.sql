-- Create table
create table TB_CENTERRESOURCE
(
  crid          NUMBER(10) not null,
  equipmentcode NVARCHAR2(50) not null,
  equipmenttype NVARCHAR2(50) not null,
  supporttask   NVARCHAR2(50) not null,
  dataprocess   NUMBER(12,2) not null,
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
comment on column TB_CENTERRESOURCE.crid
  is '���';
comment on column TB_CENTERRESOURCE.equipmentcode
  is '�豸���';
comment on column TB_CENTERRESOURCE.equipmenttype
  is '�豸����';
comment on column TB_CENTERRESOURCE.supporttask
  is '֧�ֵ�����';
comment on column TB_CENTERRESOURCE.dataprocess
  is '������ݴ�����';
comment on column TB_CENTERRESOURCE.status
  is '״̬';
comment on column TB_CENTERRESOURCE.extproperties
  is '��չ����';
comment on column TB_CENTERRESOURCE.createdtime
  is '����ʱ��';
comment on column TB_CENTERRESOURCE.createduserid
  is '�����û�';
comment on column TB_CENTERRESOURCE.updatedtime
  is '����޸�ʱ��';
comment on column TB_CENTERRESOURCE.updateduserid
  is '�޸��û�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_CENTERRESOURCE
  add constraint PK_TB_CENTERRESOURCE primary key (CRID)
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

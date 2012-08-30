-- Create table
create table TB_GROUNDRESOURCE
(
  grid          NUMBER(10) not null,
  rid           NUMBER(5) not null,
  equipmentname NVARCHAR2(50) not null,
  equipmentcode NVARCHAR2(50) not null,
  functiontype  NVARCHAR2(50) not null,
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
comment on column TB_GROUNDRESOURCE.grid
  is '���';
comment on column TB_GROUNDRESOURCE.rid
  is '����վ���';
comment on column TB_GROUNDRESOURCE.equipmentname
  is '�豸����';
comment on column TB_GROUNDRESOURCE.equipmentcode
  is '�豸���';
comment on column TB_GROUNDRESOURCE.functiontype
  is '��������';
comment on column TB_GROUNDRESOURCE.status
  is '״̬';
comment on column TB_GROUNDRESOURCE.extproperties
  is '��չ����';
comment on column TB_GROUNDRESOURCE.createdtime
  is '����ʱ��';
comment on column TB_GROUNDRESOURCE.createduserid
  is '�����û�';
comment on column TB_GROUNDRESOURCE.updatedtime
  is '����޸�ʱ��';
comment on column TB_GROUNDRESOURCE.updateduserid
  is '�޸��û�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_GROUNDRESOURCE
  add constraint PK_TB_GROUNDRESOURCE primary key (GRID)
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

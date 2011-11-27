Drop table TB_CENTEROUTPUTPOLICY;
-- Create table
create table TB_CENTEROUTPUTPOLICY
(
  copid          NUMBER(10) not null,
  taskid         NVARCHAR2(50) not null,
  satname        NVARCHAR2(50) not null,
  InfoSource     VARCHAR2(4) not null,
  infotype       VARCHAR2(4) not null,
  DDestination   VARCHAR2(4) not null,
  effecttime     DATE not null,
  defecttime     DATE not null,
  note           NVARCHAR2(200),
  createdtime    DATE not null,
  createduserid  NUMBER(10),
  updatedtime    DATE,
  updateduserid  NUMBER(10)
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
comment on column TB_CENTEROUTPUTPOLICY.copid
  is '���';
comment on column TB_CENTEROUTPUTPOLICY.taskid
  is '�������';
comment on column TB_CENTEROUTPUTPOLICY.satname
  is '��������';
comment on column TB_CENTEROUTPUTPOLICY.InfoSource
  is '��Դ';
comment on column TB_CENTEROUTPUTPOLICY.infotype
  is '��Ϣ���';
comment on column TB_CENTEROUTPUTPOLICY.DDestination
  is '����';
comment on column TB_CENTEROUTPUTPOLICY.effecttime
  is '��Чʱ��';
comment on column TB_CENTEROUTPUTPOLICY.defecttime
  is 'ʧЧʱ��';
comment on column TB_CENTEROUTPUTPOLICY.note
  is '����';
comment on column TB_CENTEROUTPUTPOLICY.createdtime
  is '����ʱ��';
comment on column TB_CENTEROUTPUTPOLICY.createduserid
  is '�����û�ID';
comment on column TB_CENTEROUTPUTPOLICY.updatedtime
  is '����޸�ʱ��';
comment on column TB_CENTEROUTPUTPOLICY.updateduserid
  is '����޸��û�ID';
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

-- Create table
create table TB_RESOURCECALCULATE
(
  rcid                       NUMBER(10) not null,
  requirementfiledirectory   NVARCHAR2(100),
  requirementfilename        NVARCHAR2(50),
  requirementfiledisplayname NVARCHAR2(50),
  resultfiledirectory        NVARCHAR2(100),
  resultfilename             NVARCHAR2(50),
  resultfiledisplayname      NVARCHAR2(50),
  resultfilesource           NUMBER(2),
  calculateresult            NUMBER(2),
  status                     NUMBER(2) not null,
  createdtime                DATE not null,
  createduserid              NUMBER(10),
  updatedtime                DATE,
  updateduserid              NUMBER(10)
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
comment on column TB_RESOURCECALCULATE.rcid
  is '���';
comment on column TB_RESOURCECALCULATE.requirementfiledirectory
  is '��Դ�����ļ�Ŀ¼';
comment on column TB_RESOURCECALCULATE.requirementfilename
  is '��Դ�����ļ�����';
comment on column TB_RESOURCECALCULATE.requirementfiledisplayname
  is '��Դ�����ļ���ʾ����';
comment on column TB_RESOURCECALCULATE.resultfiledirectory
  is '��Դ�������ļ�Ŀ¼';
comment on column TB_RESOURCECALCULATE.resultfilename
  is '��Դ�������ļ�����';
comment on column TB_RESOURCECALCULATE.resultfiledisplayname
  is '��Դ�������ļ���ʾ����';
comment on column TB_RESOURCECALCULATE.resultfilesource
  is '��Դ��������Դ��ϵͳ���㣨1�����û��ϴ���2��';
comment on column TB_RESOURCECALCULATE.calculateresult
  is '��Դ������������ɹ���1��������ʧ�ܣ�2����';
comment on column TB_RESOURCECALCULATE.status
  is '״̬���ȴ����㣨1����������ɣ�2����';
comment on column TB_RESOURCECALCULATE.createdtime
  is '����ʱ��';
comment on column TB_RESOURCECALCULATE.createduserid
  is '�����û�ID';
comment on column TB_RESOURCECALCULATE.updatedtime
  is '����޸�ʱ��';
comment on column TB_RESOURCECALCULATE.updateduserid
  is '����޸��û�ID';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_RESOURCECALCULATE
  add constraint PK_TB_RESOURCECALCULATE primary key (RCID)
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

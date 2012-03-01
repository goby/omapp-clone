--���ز�����Ҫִ�д˽ű�������ʱ�׷�DB��Ӧ�ô��ڸñ���Ӧ����
-- Create table
create table TB_INFOTYPE
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
comment on column TB_INFOTYPE.rid
  is '��¼��';
comment on column TB_INFOTYPE.dataname
  is '��Ϣ����';
comment on column TB_INFOTYPE.inmark
  is '�ڲ���ʶ';
comment on column TB_INFOTYPE.exmark
  is '�ⲿ��ʶ';
comment on column TB_INFOTYPE.incode
  is '�ڲ�����';
comment on column TB_INFOTYPE.excode
  is '�ⲿ����';
comment on column TB_INFOTYPE.datatype
  is '��Ϣ���ͣ�0-����֡��1-�ļ�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_INFOTYPE
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

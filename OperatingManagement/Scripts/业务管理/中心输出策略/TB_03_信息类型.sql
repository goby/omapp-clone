-- Create table
create table TB_XXTYPE
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
comment on column TB_XXTYPE.rid
  is '��¼��';
comment on column TB_XXTYPE.dataname
  is '��Ϣ����';
comment on column TB_XXTYPE.inmark
  is '�ڲ���ʶ';
comment on column TB_XXTYPE.exmark
  is '�ⲿ��ʶ';
comment on column TB_XXTYPE.incode
  is '�ڲ�����';
comment on column TB_XXTYPE.excode
  is '�ⲿ����';
comment on column TB_XXTYPE.datatype
  is '��Ϣ���ͣ�0-����֡��1-�ļ�';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_XXTYPE
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

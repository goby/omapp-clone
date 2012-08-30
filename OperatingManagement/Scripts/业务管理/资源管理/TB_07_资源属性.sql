-- Create table
create table TB_ZYSX
(
  id    NUMBER(4) not null,
  pname VARCHAR2(50) not null,
  pcode VARCHAR2(20) not null,
  type  NUMBER(1),
  scope VARCHAR2(20),
  own   NUMBER(1)
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
comment on column TB_ZYSX.type
  is '�������ͣ�1,int;2double;3,string;4,bool;5,enum';
comment on column TB_ZYSX.scope
  is 'ֵ���䣺type:1,0-xxxxxx;type:2,m.n;3,length;5,a,b,c,;';
comment on column TB_ZYSX.own
  is '���ڣ�0�����ǣ�1������վ��2���Ǻ͵���վ��3��������';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_ZYSX
  add constraint PK_ZYSX primary key (ID)
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

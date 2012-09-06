--------------------------------------------
-- Export file for user HTCUSER           --
-- Created by taiji on 2012/9/6, 14:46:20 --
--------------------------------------------

spool UP_01_�ƻ�����20120906.log

prompt
prompt Creating table TB_GD
prompt ====================
prompt
create table HTCUSER.TB_GD
(
  ID       NUMBER(20) not null,
  CTIME    DATE not null,
  TASKID   VARCHAR2(20),
  SATID    VARCHAR2(50),
  ITYPE    NUMBER,
  ICODE    VARCHAR2(10),
  D        NUMBER(8),
  T        NUMBER(10),
  TIMES    DATE,
  A        NUMBER(12,6),
  E        NUMBER(12,6),
  I        NUMBER(12,6),
  Q        NUMBER(12,6),
  W        NUMBER(12,6),
  M        NUMBER(12,6),
  P        NUMBER(12,6),
  PP       NUMBER(12,6),
  RA       NUMBER(12,6),
  RP       NUMBER(12,6),
  CDSM     NUMBER(12,6),
  KZ1      NUMBER(12,6),
  KSM      NUMBER(12,6),
  KZ2      NUMBER(12,6),
  RESERVE  VARCHAR2(100),
  DFINFOID NUMBER(20)
)
tablespace TSHTC
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
comment on column HTCUSER.TB_GD.SATID
  is '���Ǵ���';
comment on column HTCUSER.TB_GD.ITYPE
  is '�������ͣ�0-���ǳ�ʼ���������1-����˲ʱ���������2-�����º󾫹����';
comment on column HTCUSER.TB_GD.ICODE
  is '���ݱ���';
comment on column HTCUSER.TB_GD.D
  is '��Ԫ����';
comment on column HTCUSER.TB_GD.T
  is '��Ԫʱ��';
comment on column HTCUSER.TB_GD.TIMES
  is '��Ԫʱ�䣨��Ԫ���ں���Ԫʱ����ϵ�ʱ�䣩';
comment on column HTCUSER.TB_GD.A
  is '����볤��';
comment on column HTCUSER.TB_GD.E
  is '���ƫ����';
comment on column HTCUSER.TB_GD.I
  is '������';
comment on column HTCUSER.TB_GD.Q
  is '������ྭ����λ��';
comment on column HTCUSER.TB_GD.W
  is '���ص���ǣ���λ��';
comment on column HTCUSER.TB_GD.M
  is 'ƽ����ǣ���λΪ��';
comment on column HTCUSER.TB_GD.P
  is '������ڣ���λΪ����';
comment on column HTCUSER.TB_GD.PP
  is '������ڱ仯�ʣ���λ��/��';
comment on column HTCUSER.TB_GD.RA
  is 'Զ�ص���ľ�';
comment on column HTCUSER.TB_GD.RP
  is '���ص���ľ�';
comment on column HTCUSER.TB_GD.CDSM
  is '���������㶯ϵ�����λΪ��2/ǧ��';
comment on column HTCUSER.TB_GD.KZ1
  is '��չ��1';
comment on column HTCUSER.TB_GD.KSM
  is '��ѹ�㶯ϵ�����λΪ��2/ǧ��';
comment on column HTCUSER.TB_GD.KZ2
  is '��չ��2';
alter table HTCUSER.TB_GD
  add constraint PK_TB_GD primary key (ID)
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

prompt
prompt Creating table TB_JH
prompt ====================
prompt
create table HTCUSER.TB_JH
(
  ID        NUMBER(20) not null,
  CTIME     DATE not null,
  TASKID    VARCHAR2(20),
  PLANTYPE  VARCHAR2(20),
  PLANID    NUMBER(10),
  STARTTIME DATE,
  ENDTIME   DATE,
  SRCTYPE   NUMBER(1),
  SRCID     NUMBER(20),
  FILEINDEX VARCHAR2(100),
  RESERVE   VARCHAR2(100)
)
tablespace TSHTC
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
comment on column HTCUSER.TB_JH.SRCTYPE
  is '�ƻ�Դ���ͣ��հ׼ƻ�0���������1���豸�����ƻ�2��';
comment on column HTCUSER.TB_JH.SRCID
  is '�ƻ�Դ��ţ����ƻ�Դ����Ϊ1ʱ��Ϊ��������ţ��ƻ�Դ����Ϊ2ʱ��Ϊ�豸�����ƻ���š�';
alter table HTCUSER.TB_JH
  add constraint PK_TB_JH primary key (ID)
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

prompt
prompt Creating table TB_JHTEMP
prompt ========================
prompt
create table HTCUSER.TB_JHTEMP
(
  ID        NUMBER(20) not null,
  CTIME     DATE not null,
  TASKID    VARCHAR2(20),
  PLANTYPE  VARCHAR2(20),
  PLANID    NUMBER(10),
  STARTTIME DATE,
  ENDTIME   DATE,
  SRCTYPE   NUMBER(1),
  SRCID     NUMBER(20),
  FILEINDEX VARCHAR2(100),
  RESERVE   VARCHAR2(100)
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
comment on column HTCUSER.TB_JHTEMP.SRCTYPE
  is '�ƻ�Դ���ͣ��հ׼ƻ�0���������1���豸�����ƻ�2��';
comment on column HTCUSER.TB_JHTEMP.SRCID
  is '�ƻ�Դ��ţ����ƻ�Դ����Ϊ1ʱ��Ϊ��������ţ��ƻ�Դ����Ϊ2ʱ��Ϊ�豸�����ƻ���š�';
alter table HTCUSER.TB_JHTEMP
  add constraint PK_TB_JHTEMP primary key (ID)
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

prompt
prompt Creating table TB_SYCX
prompt ======================
prompt
create table HTCUSER.TB_SYCX
(
  ID        NUMBER(20) not null,
  CTIME     DATE not null,
  TASKID    VARCHAR2(20),
  PTYPE     VARCHAR2(20),
  PNAME     VARCHAR2(100),
  PNID      NUMBER(10),
  PLANID    NUMBER(10),
  STARTTIME DATE,
  ENDTIME   DATE,
  FILEINDEX VARCHAR2(100),
  RESERVE   VARCHAR2(100),
  INFOTYPE  VARCHAR2(50),
  LINECOUNT NUMBER(10)
)
tablespace TSHTC
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
alter table HTCUSER.TB_SYCX
  add constraint PK_TB_SYCX primary key (ID)
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

prompt
prompt Creating table TB_YDSJ
prompt ======================
prompt
create table HTCUSER.TB_YDSJ
(
  ID      NUMBER(20) not null,
  CTIME   DATE not null,
  TASKID  VARCHAR2(20),
  SATNAME VARCHAR2(50),
  D       NUMBER(8),
  T       NUMBER(10),
  TIMES   DATE,
  A       NUMBER(12,6),
  E       NUMBER(12,6),
  I       NUMBER(12,6),
  O       NUMBER(12,6),
  W       NUMBER(12,6),
  M       NUMBER(12,6),
  RESERVE VARCHAR2(100)
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
comment on column HTCUSER.TB_YDSJ.CTIME
  is '��¼����ʱ��';
comment on column HTCUSER.TB_YDSJ.TASKID
  is '�������';
comment on column HTCUSER.TB_YDSJ.SATNAME
  is '��������';
comment on column HTCUSER.TB_YDSJ.D
  is '��Ԫ����';
comment on column HTCUSER.TB_YDSJ.T
  is '��Ԫʱ��';
comment on column HTCUSER.TB_YDSJ.A
  is '����볤��';
comment on column HTCUSER.TB_YDSJ.E
  is '���ƫ����';
comment on column HTCUSER.TB_YDSJ.I
  is '������';
comment on column HTCUSER.TB_YDSJ.O
  is '���������ྶ';
comment on column HTCUSER.TB_YDSJ.W
  is '������ص����';
comment on column HTCUSER.TB_YDSJ.M
  is 'ƽ�����';
comment on column HTCUSER.TB_YDSJ.RESERVE
  is '��ע';
alter table HTCUSER.TB_YDSJ
  add constraint PK_TB_YDSJ primary key (ID)
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


spool off

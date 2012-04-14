---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/4/14, 23:15:35 --
---------------------------------------------

spool TB_01_�ƻ�����20120414.log

prompt
prompt Creating table TB_GD
prompt ====================
prompt
create table HTCUSER.TB_GD
(
  ID      NUMBER(20) not null,
  CTIME   DATE not null,
  SATID   VARCHAR2(50),
  ITYPE   VARCHAR2(10),
  ICODE   VARCHAR2(10),
  D       DATE,
  T       NUMBER(12),
  TIMES   DATE,
  A       NUMBER(12,4),
  E       NUMBER(12,6),
  I       NUMBER(12,6),
  Q       NUMBER(12,6),
  W       NUMBER(12,6),
  M       NUMBER(12,6),
  DELTP   NUMBER(12,6),
  P       NUMBER(12,6),
  PP      NUMBER(12,6),
  RA      NUMBER(12,6),
  RP      NUMBER(12,6),
  CDSM    NUMBER(12,6),
  KZ1     NUMBER(12,6),
  KSM     NUMBER(12,6),
  KZ2     NUMBER(12,6),
  RESERVE VARCHAR2(100)
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
  TASKID  VARCHAR2(10),
  SATNAME VARCHAR2(32),
  D       DATE,
  T       VARCHAR2(10),
  A       NUMBER(18,6),
  E       NUMBER(18,6),
  I       NUMBER(18,6),
  O       NUMBER(18,6),
  W       NUMBER(18,6),
  M       NUMBER(18,6),
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

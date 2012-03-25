---------------------------------------------
-- Export file for user HTCUSER            --
-- Created by taiji on 2012/3/13, 21:32:28 --
---------------------------------------------

spool TB_01_计划管理20120313.log

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
  PLANID    NUMBER(4),
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
  is '计划源类型：空白计划0；试验程序1；设备工作计划2；';
comment on column HTCUSER.TB_JH.SRCID
  is '计划源编号：当计划源类型为1时，为试验程序编号；计划源类型为2时，为设备工作计划编号。';
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
prompt Creating table TB_PLAN
prompt ======================
prompt
create table HTCUSER.TB_PLAN
(
  PLANID    NUMBER(20) not null,
  PLANTYPE  VARCHAR2(20) not null,
  PLANAGING VARCHAR2(20),
  SYCXID    NUMBER(20) not null,
  STARTTIME DATE,
  ENDTIME   DATE,
  FILEINDEX VARCHAR2(100),
  RESERVE   VARCHAR2(100),
  CTIME     DATE not null
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
alter table HTCUSER.TB_PLAN
  add constraint PK_TB_PLAN primary key (PLANID)
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
  ID         NUMBER(20) not null,
  CTIME      DATE not null,
  SPACETYPE  VARCHAR2(1),
  RESERVE    VARCHAR2(100),
  DATA_D     DATE,
  DATA_T     NUMBER(12),
  DATA_A     NUMBER(18,6),
  DATA_E     NUMBER(18,6),
  DATA_I     NUMBER(18,6),
  DATA_OHM   NUMBER(18,6),
  DATA_OMEGA NUMBER(18,6),
  DATA_M     NUMBER(18,6)
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
comment on column HTCUSER.TB_YDSJ.SPACETYPE
  is '1:空间机动任务;2:非空间机动任务';
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

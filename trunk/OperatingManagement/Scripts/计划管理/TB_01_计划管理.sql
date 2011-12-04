--------------------------------------------
-- Export file for user HTCUSER           --
-- Created by taiji on 2011/12/5, 0:29:31 --
--------------------------------------------

spool TB_01_计划管理.log

prompt
prompt Creating table TB_GD
prompt ====================
prompt
create table HTCUSER.TB_GD
(
  ID         NUMBER(20) not null,
  CTIME      DATE not null,
  RESERVE    VARCHAR2(100),
  DATA_D     VARCHAR2(10),
  DATA_T     VARCHAR2(10),
  DATA_A     VARCHAR2(10),
  DATA_E     VARCHAR2(10),
  DATA_I     VARCHAR2(10),
  DATA_OHM   VARCHAR2(10),
  DATA_OMEGA VARCHAR2(10),
  DATA_M     VARCHAR2(10),
  DATA_P     VARCHAR2(10),
  DATA_PI    VARCHAR2(10),
  DATA_RA    VARCHAR2(10),
  DATA_RP    VARCHAR2(10),
  DATA_ID    NUMBER(20)
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
prompt Creating table TB_GDSH
prompt ======================
prompt
create table HTCUSER.TB_GDSH
(
  ID         NUMBER(20) not null,
  CTIME      DATE not null,
  RESERVE    VARCHAR2(100),
  DATA_D     VARCHAR2(10),
  DATA_T     VARCHAR2(10),
  DATA_A     VARCHAR2(10),
  DATA_E     VARCHAR2(10),
  DATA_I     VARCHAR2(10),
  DATA_OHM   VARCHAR2(10),
  DATA_OMEGA VARCHAR2(10),
  DATA_M     VARCHAR2(10),
  DATA_CDSM  VARCHAR2(10),
  DATA_KSM   VARCHAR2(10),
  DATA_KZ1   VARCHAR2(10),
  DATA_KZ2   VARCHAR2(10),
  DATA_ID    NUMBER(20)
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
alter table HTCUSER.TB_GDSH
  add constraint PK_TB_GDSH primary key (ID)
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
prompt Creating table TB_GZJH
prompt ======================
prompt
create table HTCUSER.TB_GZJH
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
  FORMAT1     VARCHAR2(100),
  FORMAT2     VARCHAR2(100),
  DATASECTION VARCHAR2(500),
  FILEINDEX   VARCHAR2(100),
  RESERVE     VARCHAR2(100)
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
alter table HTCUSER.TB_GZJH
  add constraint PK_TB_GZJH primary key (ID)
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
prompt Creating table TB_SYJH
prompt ======================
prompt
create table HTCUSER.TB_SYJH
(
  JHID        NUMBER(20) not null,
  CTIME       DATE not null,
  TASKID      VARCHAR2(20),
  PLANTYPE    VARCHAR2(20),
  PLANID      NUMBER(10),
  STARTTIME   DATE,
  ENDTIME     DATE,
  FILEINDEX   VARCHAR2(100),
  RESERVE     VARCHAR2(100),
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10)
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
alter table HTCUSER.TB_SYJH
  add constraint PK_TB_SYJH primary key (JHID)
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
prompt Creating table TB_TYSJ
prompt ======================
prompt
create table HTCUSER.TB_TYSJ
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
  FORMAT1     VARCHAR2(100),
  DATASECTION VARCHAR2(500),
  FILEINDEX   VARCHAR2(100),
  RESERVE     VARCHAR2(100)
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
alter table HTCUSER.TB_TYSJ
  add constraint PK_TB_TYSJ primary key (ID)
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
prompt Creating table TB_XXXQ
prompt ======================
prompt
create table HTCUSER.TB_XXXQ
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
  FORMAT1     VARCHAR2(100),
  FORMAT2     VARCHAR2(100),
  DATASECTION VARCHAR2(500),
  FILEINDEX   VARCHAR2(100),
  RESERVE     VARCHAR2(100)
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
alter table HTCUSER.TB_XXXQ
  add constraint PK_TB_XXXQ primary key (ID)
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
  DATA_D     VARCHAR2(10),
  DATA_T     VARCHAR2(10),
  DATA_A     VARCHAR2(10),
  DATA_E     VARCHAR2(10),
  DATA_I     VARCHAR2(10),
  DATA_OHM   VARCHAR2(10),
  DATA_OMEGA VARCHAR2(10),
  DATA_M     VARCHAR2(10),
  DATA_ID    NUMBER(20)
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

prompt
prompt Creating table TB_YJJH
prompt ======================
prompt
create table HTCUSER.TB_YJJH
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
  FORMAT1     VARCHAR2(100),
  DATASECTION VARCHAR2(500),
  FILEINDEX   VARCHAR2(100),
  RESERVE     VARCHAR2(100)
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
alter table HTCUSER.TB_YJJH
  add constraint PK_TB_YJJH primary key (ID)
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

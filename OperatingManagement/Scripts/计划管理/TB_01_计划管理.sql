----------------------------------------------
-- Export file for user HTCUSER             --
-- Created by taiji on 2011/12/12, 23:58:27 --
----------------------------------------------

spool TB_01_计划管理.log

prompt
prompt Creating table TB_AE
prompt ====================
prompt
create table HTCUSER.TB_AE
(
  ID           NUMBER(20) not null,
  CTIME        DATE not null,
  RESERVE      VARCHAR2(100),
  DATA_ZT      VARCHAR2(10),
  DATA_T       VARCHAR2(10),
  DATA_A       VARCHAR2(10),
  DATA_E       VARCHAR2(10),
  DATA_DELTAA1 VARCHAR2(10),
  DATA_DELTAE1 VARCHAR2(10),
  DATA_DELTAA2 VARCHAR2(10),
  DATA_DELTAE2 VARCHAR2(10),
  DATA_SPHI    VARCHAR2(10),
  DATA_ID      NUMBER(20)
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
alter table HTCUSER.TB_AE
  add constraint PK_TB_AE primary key (ID)
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
prompt Creating table TB_BASICINFODATA
prompt ===============================
prompt
create table HTCUSER.TB_BASICINFODATA
(
  ID                 NUMBER(20) not null,
  CTIME              DATE,
  VERSION            VARCHAR2(50),
  FLAG               VARCHAR2(50),
  MAINTYPE           VARCHAR2(50),
  DATATYPE           VARCHAR2(50),
  SOURCEADDRESS      VARCHAR2(50),
  DESTINATIONADDRESS VARCHAR2(50),
  MISSIONCODEV       VARCHAR2(50),
  SATELLITECODE      VARCHAR2(50),
  DATADATE           DATE,
  DATATIME           VARCHAR2(50),
  SEQUENCENUMBER     VARCHAR2(50),
  CHILDRENPACKNUMBER VARCHAR2(50),
  UDPRESERVE         VARCHAR2(50),
  DATALENGTH         VARCHAR2(50),
  DATACLASS          VARCHAR2(50),
  TB_TABLE           VARCHAR2(20)
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
alter table HTCUSER.TB_BASICINFODATA
  add constraint PK_TB_BASICINFODATA primary key (ID)
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
prompt Creating table TB_CZJG
prompt ======================
prompt
create table HTCUSER.TB_CZJG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_CZJG
  add constraint PK_TB_CZJG primary key (ID)
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
prompt Creating table TB_CZZT
prompt ======================
prompt
create table HTCUSER.TB_CZZT
(
  ID         NUMBER(20) not null,
  CTIME      DATE not null,
  RESERVE    VARCHAR2(100),
  DATA_BJ    VARCHAR2(10),
  DATA_ZX    VARCHAR2(10),
  DATA_T0    VARCHAR2(10),
  DATA_T1    VARCHAR2(10),
  DATA_A     VARCHAR2(10),
  DATA_E     VARCHAR2(10),
  DATA_I     VARCHAR2(10),
  DATA_OHM   VARCHAR2(10),
  DATA_OMEGA VARCHAR2(10),
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
alter table HTCUSER.TB_CZZT
  add constraint PK_TB_CZZT primary key (ID)
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
prompt Creating table TB_GCJG
prompt ======================
prompt
create table HTCUSER.TB_GCJG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_GCJG
  add constraint PK_TB_GCJG primary key (ID)
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
prompt Creating table TB_GDXA
prompt ======================
prompt
create table HTCUSER.TB_GDXA
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_GDXA
  add constraint PK_TB_GDXA primary key (ID)
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
prompt Creating table TB_HJXX
prompt ======================
prompt
create table HTCUSER.TB_HJXX
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_HJXX
  add constraint PK_TB_HJXX primary key (ID)
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
prompt Creating table TB_JDJG
prompt ======================
prompt
create table HTCUSER.TB_JDJG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_JDJG
  add constraint PK_TB_JDJG primary key (ID)
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
prompt Creating table TB_JHBG
prompt ======================
prompt
create table HTCUSER.TB_JHBG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_JHBG
  add constraint PK_TB_JHBG primary key (ID)
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
prompt Creating table TB_MBXX
prompt ======================
prompt
create table HTCUSER.TB_MBXX
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_MBXX
  add constraint PK_TB_MBXX primary key (ID)
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
prompt Creating table TB_R
prompt ===================
prompt
create table HTCUSER.TB_R
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  RESERVE     VARCHAR2(100),
  DATA_ZT     VARCHAR2(10),
  DATA_T      VARCHAR2(10),
  DATA_RE     VARCHAR2(10),
  DATA_DELTAT VARCHAR2(10),
  DATA_SPHI   VARCHAR2(10),
  DATA_ID     NUMBER(20)
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
alter table HTCUSER.TB_R
  add constraint PK_TB_R primary key (ID)
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
prompt Creating table TB_RR
prompt ====================
prompt
create table HTCUSER.TB_RR
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  RESERVE     VARCHAR2(100),
  DATA_ZT     VARCHAR2(10),
  DATA_T      VARCHAR2(10),
  DATA_RR     VARCHAR2(10),
  DATA_DELTAF VARCHAR2(10),
  DATA_SPHI   VARCHAR2(10),
  DATA_ID     NUMBER(20)
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
alter table HTCUSER.TB_RR
  add constraint PK_TB_RR primary key (ID)
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
prompt Creating table TB_SBJH
prompt ======================
prompt
create table HTCUSER.TB_SBJH
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_SBJH
  add constraint PK_TB_SBJH primary key (ID)
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
prompt Creating table TB_SYXQ
prompt ======================
prompt
create table HTCUSER.TB_SYXQ
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_SYXQ
  add constraint PK_TB_SYXQ primary key (ID)
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
prompt Creating table TB_T0
prompt ====================
prompt
create table HTCUSER.TB_T0
(
  ID         NUMBER(20) not null,
  CTIME      DATE not null,
  RESERVE    VARCHAR2(100),
  DATA_TZERO VARCHAR2(10),
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
alter table HTCUSER.TB_T0
  add constraint PK_TB_T0 primary key (ID)
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
prompt Creating table TB_TYJG
prompt ======================
prompt
create table HTCUSER.TB_TYJG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_TYJG
  add constraint PK_TB_TYJG primary key (ID)
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
prompt Creating table TB_UDPDATABASICINFO
prompt ==================================
prompt
create table HTCUSER.TB_UDPDATABASICINFO
(
  ID                 NUMBER(20) not null,
  CTIME              DATE not null,
  VERSION            VARCHAR2(2),
  FLAG               VARCHAR2(2),
  MAINTYPE           VARCHAR2(20),
  DATATYPE           VARCHAR2(20),
  SOURCE             VARCHAR2(50),
  DESTINATION        VARCHAR2(50),
  MISSIONCODE        VARCHAR2(50),
  SATELLITECODE      VARCHAR2(20),
  DATADATE           DATE,
  DATATIME           VARCHAR2(20),
  SEQUENCENUMBER     VARCHAR2(20),
  CHILDRENPACKNUMBER VARCHAR2(20),
  RESERVE            VARCHAR2(20),
  DATALENGTH         VARCHAR2(100),
  DATACLASS          VARCHAR2(10)
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
alter table HTCUSER.TB_UDPDATABASICINFO
  add constraint PK_TB_UDPDATABASICINFO primary key (ID)
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
prompt Creating table TB_XDSC
prompt ======================
prompt
create table HTCUSER.TB_XDSC
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  RESERVE     VARCHAR2(100),
  DATA_D      DATE,
  DATA_T      VARCHAR2(10),
  DATA_N      VARCHAR2(10),
  DATA_DELTAT VARCHAR2(10),
  DATA_ID     NUMBER(20)
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
alter table HTCUSER.TB_XDSC
  add constraint PK_TB_XDSC primary key (ID)
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
prompt Creating table TB_YJBG
prompt ======================
prompt
create table HTCUSER.TB_YJBG
(
  ID          NUMBER(20) not null,
  CTIME       DATE not null,
  SOURCE      VARCHAR2(50),
  DESTINATION VARCHAR2(50),
  TASKID      VARCHAR2(50),
  INFOTYPE    VARCHAR2(50),
  LINECOUNT   NUMBER(10),
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
alter table HTCUSER.TB_YJBG
  add constraint PK_TB_YJBG primary key (ID)
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

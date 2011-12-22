


create or replace procedure UP_CenterRes_SelectByID
(
       p_CRID TB_CenterResource.CRID%type,
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_CenterResource Where CRID=p_CRID;
end;


create or replace procedure UP_CenterRes_SelectAll
(
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_CenterResource Order By CreatedTime Desc;
end;


create or replace procedure UP_CenterRes_Insert
(
       p_EquipmentCode TB_CenterResource.EquipmentCode%type,
       p_EquipmentType TB_CenterResource.EquipmentType%type,
       p_SupportTask TB_CenterResource.SupportTask%type,
       p_DataProcess TB_CenterResource.DataProcess%type,
       p_Status TB_CenterResource.Status%type,
       p_ExtProperties TB_CenterResource.ExtProperties%type,
       p_CreatedTime TB_CenterResource.Createdtime%type,
       p_CreatedUserID TB_CenterResource.Createduserid%type,
       p_UpdatedTime TB_CenterResource.Updatedtime%type,
       p_UpdatedUserID TB_CenterResource.Updateduserid%type,
       v_CRID out TB_CenterResource.CRID%type,
       v_Result out number
)
is
begin  
       savepoint p1;
       v_CRID:=to_number(fn_genseqnum('4004'));
       Insert into TB_CenterResource(CRID,EquipmentCode,EquipmentType,SupportTask,DataProcess,Status,ExtProperties,CreatedTime,CreatedUserID,UpdatedTime,UpdatedUserID) 
       Values(v_CRID,p_EquipmentCode,p_EquipmentType,p_SupportTask,p_DataProcess,p_Status,p_ExtProperties,p_CreatedTime,p_CreatedUserID,p_UpdatedTime,p_UpdatedUserID);
       commit;
       v_Result:=5; -- Success
       
       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
       v_Result:=4; --Error
end;



create or replace procedure UP_CenterRes_Update
(
       p_CRID TB_CenterResource.CRID%type,
       p_EquipmentCode TB_CenterResource.EquipmentCode%type,
       p_EquipmentType TB_CenterResource.EquipmentType%type,
       p_SupportTask TB_CenterResource.SupportTask%type,
       p_DataProcess TB_CenterResource.DataProcess%type,
       p_Status TB_CenterResource.Status%type,
       p_ExtProperties TB_CenterResource.ExtProperties%type,
       p_CreatedTime TB_CenterResource.Createdtime%type,
       p_CreatedUserID TB_CenterResource.Createduserid%type,
       p_UpdatedTime TB_CenterResource.Updatedtime%type,
       p_UpdatedUserID TB_CenterResource.Updateduserid%type,
       v_Result out number
)
is
begin
     savepoint p1;
     
       Update TB_CenterResource
       Set EquipmentCode=p_EquipmentCode
          ,EquipmentType=p_EquipmentType
          ,SupportTask=p_SupportTask
          ,DataProcess=p_DataProcess
          ,Status=p_Status
          ,ExtProperties=p_ExtProperties
          ,CreatedTime=p_CreatedTime
          ,CreatedUserID=p_CreatedUserID
          ,UpdatedTime=p_UpdatedTime
          ,UpdatedUserID=p_UpdatedUserID
        Where CRID=p_CRID;
        Commit;
        v_Result:=5; -- Success
       
        EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
        v_Result:=4; --Error
end;


create or replace procedure UP_CenterRes_Search
(
       p_Status varchar2,
       p_TimePoint date,
       o_Cursor out sys_refcursor
)
is
begin
       IF p_Status='' Or p_Status Is Null Then--ȫ�� 
         open o_Cursor for
             Select * From TB_CenterResource
             Order By CreatedTime Desc;
       Elsif p_Status='4' Then---ɾ��
         open o_Cursor for
             Select * From TB_CenterResource
             Where Status=2
             Order By CreatedTime Desc;
       Elsif p_Status='1' Then --����
         open o_Cursor for
             Select * From TB_CenterResource
             Where Status=1
               And CRID not in (Select ResourceID From TB_HEALTHSTATUS
                                 Where ResourceType=3
                                   And Status=2
                                   And BeginTime<=p_TimePoint
                                   And EndTime>=p_TimePoint)
             Order By CreatedTime Desc;
       Elsif p_Status='2' Then --�쳣
          open o_Cursor for
             Select * From TB_CenterResource
                 Where Status=1
                   And CRID in (Select ResourceID From TB_HEALTHSTATUS
                                 Where ResourceType=3
                                   And Status=2
                                   And BeginTime<=p_TimePoint
                                   And EndTime>=p_TimePoint)
                 Order By CreatedTime Desc;
       Elsif p_Status='3' Then --ռ����
          open o_Cursor for
             Select * From TB_CenterResource
                 Where Status=1
                   And CRID in (Select ResourceID From TB_USESTATUS
                                 Where ResourceType=3
                                   And BeginTime<=p_TimePoint
                                   And EndTime>=p_TimePoint)
                 Order By CreatedTime Desc;

       End IF;
end;




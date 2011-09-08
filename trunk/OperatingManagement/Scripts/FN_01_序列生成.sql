create or replace function generateseqnumber(SEQTYPE   IN VARCHAR2)
 return varchar2 as
  SEQNUMBER varchar2(50);
  CVALUE     NUMBER(20) := 0;
  MVALUE     NUMBER(20) := 0;
  SVALUE     NUMBER(10) := 0;
  PRESTRING  VARCHAR(10) := '';
  POSTSTRING VARCHAR(10) := '';
begin
  --===================================================
  --
  -- ������������ָ�����͵����к�
  --
  --===================================================
  SEQNUMBER := '';

  SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

  -- ��ȡָ�������������ӵĵ�ǰֵ
  BEGIN
    SELECT CURRENTVALUE, MAXVALUE, STEPVALUE, PREFIXSTRING, POSTFIXSTRING
      INTO CVALUE, MVALUE, SVALUE, PRESTRING, POSTSTRING
      FROM TB_SEQUENCENUMBER
     WHERE SEQUENCETYPE = SEQTYPE;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      ROLLBACK;
      RETURN '';
  END;

  -- ������ֵ�������������������ӵ�ǰֵ
  BEGIN
    IF CVALUE + SVALUE > MVALUE THEN
      --�Ƿ񳬹����ֵ
      ROLLBACK;
      RETURN '';
    END IF;
    SEQNUMBER := TO_CHAR(CVALUE + SVALUE);
    IF PRESTRING IS NOT NULL THEN
      --ʹ��ǰ׺
      SEQNUMBER := PRESTRING || SEQNUMBER;
    END IF;
    IF POSTSTRING IS NOT NULL THEN
      --ʹ�ú�׺
      SEQNUMBER := SEQNUMBER || POSTSTRING;
    END IF;

    UPDATE TB_SEQUENCENUMBER
       SET CURRENTVALUE = CVALUE + SVALUE
     WHERE SEQUENCETYPE = SEQTYPE;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RETURN '';
  END;

  COMMIT;
  return(SEQNUMBER);
end generateseqnumber;
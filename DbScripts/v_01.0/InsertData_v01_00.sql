	declare @CodeCriteria nvarchar(max)
	declare @EN_TranslationCriteriaValue nvarchar(max)
	declare @SR_TranslationCriteriaValue nvarchar(max)

	declare @IsActive bit = 1
	declare @NextCriteriaIdSystemLabel int = cast((select isnull(max(IdLabel), 0) from Label) as int) + 1
	declare @NextCriteriaSequenceOrder int = cast((select isnull(max(SequenceOrder), 0) from Criteria) as int) + 10

	-------------------------------------------------------------------------------------------------
	set @CodeCriteria = 'LANGUAGE'
	set @EN_TranslationCriteriaValue = 'Language'
	set @SR_TranslationCriteriaValue = 'Језик'

	if not exists(select * from Criteria where CodeCriteria = @CodeCriteria and IsActive = -1)
	begin
		insert into Criteria(CodeCriteria, IsActive, SequenceOrder, RefLabelCriteriaName, DateTimeCreatedOn)
		values
			(@CodeCriteria, @IsActive, @NextCriteriaSequenceOrder, @NextCriteriaIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_LANGUAGE', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())
	end

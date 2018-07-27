	declare @CodeCriteria nvarchar(max)
	declare @EN_TranslationCriteriaValue nvarchar(max)
	declare @SR_TranslationCriteriaValue nvarchar(max)

	declare @IsActive bit = 1
	declare @NextCriteriaIdSystemLabel int = cast((select isnull(max(IdLabel), 0) from Label) as int) + 1
	declare @NextCriteriaSequenceOrder int = cast((select isnull(max(SequenceOrder), 0) from Criteria) as int) + 10
	declare @IdCriteriaLanguage int = (select IdCriteria from Criteria where CodeCriteria = 'LANGUAGE')
	declare @English int = (select IdCriteriaValue from CriteriaValue where RefCriteria = @IdCriteriaLanguage and CodeCriteriaValue = 'ENGLISH')
	declare @Serbian int = (select IdCriteriaValue from CriteriaValue where RefCriteria = @IdCriteriaLanguage and CodeCriteriaValue = 'SERBIAN')

	-------------------------------------------------------------------------------------------------
	set @CodeCriteria = 'MONTH'
	set @EN_TranslationCriteriaValue = 'Month'
	set @SR_TranslationCriteriaValue = 'Месец'

	if not exists(select * from Criteria where CodeCriteria = @CodeCriteria and IsActive = -1)
	begin
		insert into Criteria(CodeCriteria, IsActive, SequenceOrder, RefLabelCriteriaName, DateTimeCreatedOn)
		values
			(@CodeCriteria, @IsActive, @NextCriteriaSequenceOrder, @NextCriteriaIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_MONTH', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaIdSystemLabel, @English, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaIdSystemLabel, @Serbian, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaSequenceOrder = @NextCriteriaSequenceOrder + 10
		set @NextCriteriaIdSystemLabel = @NextCriteriaIdSystemLabel + 1
	end

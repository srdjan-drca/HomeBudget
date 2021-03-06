	declare @CodeCriteriaValue nvarchar(max)
	declare @EN_TranslationCriteriaValue nvarchar(max)
	declare @SR_TranslationCriteriaValue nvarchar(max)

	declare @IsActive bit = 1
	declare @IdCriteriaLanguage int = (select IdCriteria from Criteria where CodeCriteria = 'LANGUAGE')
	declare @NextCriteriaValueIdSystemLabel int = cast((select isnull(max(IdLabel), 0) from Label) as int) + 1
	declare @NextCriteriaValueSequenceOrder int = cast((select isnull(max(SequenceOrder), 0) from CriteriaValue where RefCriteria = @IdCriteriaLanguage) as int) + 10

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'ENGLISH'
	set @EN_TranslationCriteriaValue = 'English'
	set @SR_TranslationCriteriaValue = 'Енглески'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaLanguage)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaLanguage, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_ENGLISH', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'SERBIAN'
	set @EN_TranslationCriteriaValue = 'Serbian'
	set @SR_TranslationCriteriaValue = 'Српски'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaLanguage)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaLanguage, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_SERBIAN', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end
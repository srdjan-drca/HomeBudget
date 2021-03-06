	declare @CodeCriteriaValue nvarchar(max)
	declare @EN_TranslationCriteriaValue nvarchar(max)
	declare @SR_TranslationCriteriaValue nvarchar(max)

	declare @IsActive bit = 1
	declare @IdCriteriaCostType int = (select IdCriteria from Criteria where CodeCriteria = 'COST_TYPE')
	declare @NextCriteriaValueIdSystemLabel int = cast((select isnull(max(IdLabel), 0) from Label) as int) + 1
	declare @NextCriteriaValueSequenceOrder int = cast((select isnull(max(SequenceOrder), 0) from CriteriaValue where RefCriteria = @IdCriteriaCostType) as int) + 10

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'BILL_SERVICING'
	set @EN_TranslationCriteriaValue = 'Bill servicing'
	set @SR_TranslationCriteriaValue = 'Одржавање рачуна'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaCostType)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaCostType, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_BILL_SERVICING', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'MASTERATA_SERVICING'
	set @EN_TranslationCriteriaValue = 'Masterata servicing'
	set @SR_TranslationCriteriaValue = 'Одржавање мастерате'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaCostType)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaCostType, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_MASTERATA_SERVICING', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'HOME_INSURANCE'
	set @EN_TranslationCriteriaValue = 'Home insurance'
	set @SR_TranslationCriteriaValue = 'Осигурање дома'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaCostType)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaCostType, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_HOME_INSURANCE', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'HOUSING_LOAN'
	set @EN_TranslationCriteriaValue = 'Housing loan'
	set @SR_TranslationCriteriaValue = 'Стамбени кредит'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaCostType)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaCostType, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_HOUSING_LOAN', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'MASTERATA'
	set @EN_TranslationCriteriaValue = 'Masterata'
	set @SR_TranslationCriteriaValue = 'Мастерата'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaCostType)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaCostType, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_MASTERATA', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

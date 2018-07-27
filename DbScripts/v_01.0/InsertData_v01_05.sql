	declare @CodeCriteriaValue nvarchar(max)
	declare @EN_TranslationCriteriaValue nvarchar(max)
	declare @SR_TranslationCriteriaValue nvarchar(max)

	declare @IsActive bit = 1
	declare @IdCriteriaMonth int = (select IdCriteria from Criteria where CodeCriteria = 'MONTH')
	declare @NextCriteriaValueIdSystemLabel int = cast((select isnull(max(IdLabel), 0) from Label) as int) + 1
	declare @NextCriteriaValueSequenceOrder int = cast((select isnull(max(SequenceOrder), 0) from CriteriaValue where RefCriteria = @IdCriteriaMonth) as int) + 10

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'JANUARY'
	set @EN_TranslationCriteriaValue = 'January'
	set @SR_TranslationCriteriaValue = 'Јануар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_JANUARY', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'FEBRUARY'
	set @EN_TranslationCriteriaValue = 'February'
	set @SR_TranslationCriteriaValue = 'Фебруар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_FEBRUARY', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'MARCH'
	set @EN_TranslationCriteriaValue = 'March'
	set @SR_TranslationCriteriaValue = 'Март'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_MARCH', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'APRIL'
	set @EN_TranslationCriteriaValue = 'April'
	set @SR_TranslationCriteriaValue = 'Април'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_APRIL', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'MAY'
	set @EN_TranslationCriteriaValue = 'May'
	set @SR_TranslationCriteriaValue = 'Мај'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_MAY', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'JUNE'
	set @EN_TranslationCriteriaValue = 'June'
	set @SR_TranslationCriteriaValue = 'Јун'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_JUNE', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'JULY'
	set @EN_TranslationCriteriaValue = 'July'
	set @SR_TranslationCriteriaValue = 'Јул'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_JULY', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

		-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'AUGUST'
	set @EN_TranslationCriteriaValue = 'August'
	set @SR_TranslationCriteriaValue = 'Август'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_AUGUST', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'SEPTEMBER'
	set @EN_TranslationCriteriaValue = 'September'
	set @SR_TranslationCriteriaValue = 'Септембар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_SEPTEMBER', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'OCTOBER'
	set @EN_TranslationCriteriaValue = 'October'
	set @SR_TranslationCriteriaValue = 'Октобар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_OCTOBER', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'NOVEMBER'
	set @EN_TranslationCriteriaValue = 'November'
	set @SR_TranslationCriteriaValue = 'Новембар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_NOVEMBER', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end

	-------------------------------------------------------------------------------------------------
	set @CodeCriteriaValue = 'DECEMBER'
	set @EN_TranslationCriteriaValue = 'December'
	set @SR_TranslationCriteriaValue = 'Децембар'

	if not exists(select * from CriteriaValue where CodeCriteriaValue = @CodeCriteriaValue and IsActive = -1 and RefCriteria = @IdCriteriaMonth)
	begin
		insert into CriteriaValue(CodeCriteriaValue, IsActive, SequenceOrder, RefCriteria, RefLabelCriteriaValueName, DateTimeCreatedOn)
		values
			(@CodeCriteriaValue, @IsActive, @NextCriteriaValueSequenceOrder, @IdCriteriaMonth, @NextCriteriaValueIdSystemLabel, getdate())

		insert into Label(CodeLabel, IsActive, DateTimeCreatedOn)
		values
			('UI_DECEMBER', @IsActive, getdate())

		insert into LabelTranslation(IsActive, RefLabel, RefCriteriaValueLanguage, Value, DateTimeCreatedOn)
		values
			(@IsActive, @NextCriteriaValueIdSystemLabel, 1, @EN_TranslationCriteriaValue, getdate()),
			(@IsActive, @NextCriteriaValueIdSystemLabel, 2, @SR_TranslationCriteriaValue, getdate())

		set @NextCriteriaValueIdSystemLabel = @NextCriteriaValueIdSystemLabel + 1
		set @NextCriteriaValueSequenceOrder = @NextCriteriaValueSequenceOrder + 10
	end
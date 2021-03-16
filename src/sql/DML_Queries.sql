--use BD_Project;

-- Paciente --

	-- Indexes
	create index pacName on ProjetoConsultorio.Paciente(Nome);

	-- Search
	go	-- paciente nome
	create proc searchByName @user varchar(50)
	as
		select * from ProjetoConsultorio.Paciente where Nome like '%'+@user+'%';
	go

	go	-- paciente subsistema 
	create proc searchBySubsist @sist varchar(100)
	as
		select * from ProjetoConsultorio.Paciente where Subsistema like '%'+@sist+'%';
	go

	go	-- paciente nif
	create proc searchByNIF @nif int
	as
		select * from ProjetoConsultorio.Paciente where PacNIF=@nif;
	go

	go	-- paciente basic info
	create proc searchBasicInfoByNIF @nif int
	as
		select Nome,DataNascimento,EstadoCivil from ProjetoConsultorio.Paciente where PacNIF=@nif;
	go

	go	-- paciente todos
	create proc searchAllPac
	as
		select * from ProjetoConsultorio.Paciente;
	go


	-- Delete
	go
	create trigger deletePac on ProjetoConsultorio.Paciente
	instead of delete
	as
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedPaciente'))) begin
			create table ProjetoConsultorio.DeletedPaciente(
				ID				int identity(1,1) not null primary key,
				PacNIF			int	not null,
				Nome			varchar(50),
				DataNascimento	date,
				Residencia		varchar(50),
				CodigoPostal	varchar(20),
				Contactos		varchar(50),
				Mail			varchar(50),
				Profissao		varchar(50),
				EstadoCivil		varchar(20),
	
				SS				int,
				SNS				int,
				Subsistema		varchar(100),
	
			);
		end
		insert into ProjetoConsultorio.DeletedPaciente select * from deleted;
		delete from ProjetoConsultorio.Paciente where PacNIF=(select PacNIF from deleted);
	go


	go
	create proc deletePacProcedure @user int
	as
	begin
		begin try
			begin tran

				exec deleteAllDescrFromUser @user;												-- analises

				exec deleteAllReqFromUser @user;												-- requisicoes

				declare @numeroconsulta as int;													-- gravidez
				declare d cursor fast_forward
				for select NumeroGravidez from ProjetoConsultorio.Gravidez where NIFPac=@user;
				open d; fetch d into @numeroconsulta;
				while @@FETCH_STATUS = 0 begin
					exec deleteGravProcedure @user, @numeroconsulta ;
					fetch d into @numeroconsulta;
				end
				close d; deallocate d;


				declare @idconsulta as int;														-- consultas
				declare c cursor fast_forward
				for select ID from ProjetoConsultorio.Consulta where NIFPaciente=@user;
				open c; fetch c into @idconsulta;
				while @@FETCH_STATUS = 0 begin
					declare @tc as int;
					select @tc = (select dbo.tipoConsulta(@idconsulta));
					exec deleteConProcedure @tc, @idconsulta ;
					fetch c into @idconsulta;
				end
				close c; deallocate c;


				delete from ProjetoConsultorio.HistorialClinico where PacNIF=@user;				-- historial clinico

				delete from ProjetoConsultorio.Paciente where PacNIF=@user;						-- paciente
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran 
		end catch
	end
	go


-- Historial Clinico --
	
	-- Search
	go	-- historial clinico nif
	create proc searchPacHC @nif int
	as 
		select * from ProjetoConsultorio.HistorialClinico where PacNIF=@nif;
	go
	
	-- Update
	go
	create trigger updateHC on ProjetoConsultorio.HistorialClinico
	after insert, update
	as
	begin	-- se gravidezes < numero de tuplos em Gravidez => gravidezes=numero de tuplos
		if((select Gravidezes from inserted) > (select count(*) from Gravidez join Paciente on NIFPac=PacNIF where PacNIF=(select PacNIF from inserted)))
			update ProjetoConsultorio.HistorialClinico set Gravidezes=(select count(*) from Gravidez join Paciente on NIFPac=PacNIF where PacNIF=(select PacNIF from inserted)) where PacNIF=(select PacNIF from inserted);
	end
	go

	

	-- Delete
	go
	create trigger deleteHC on ProjetoConsultorio.HistorialClinico
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedHistorialClinico'))) begin
			create table ProjetoConsultorio.DeletedHistorialClinico (
				ID				int identity(1,1) not null primary key,
				PacNIF				int			not null,
				GrupoSanguineo		varchar(10)		not null,
				Altura				real,
				Peso				real,
				AntFamiliares		varchar(500),
				AntPessoais			varchar(500),
				Cirurgias			varchar(500),
				Alergias			varchar(500),
				TransfSanguineas	bit,
				Tabaco				varchar(500),
				Alcool				varchar(500),
				Medicamentos		varchar(500),
				Menarca				int,
				Coitarca			int,
				Ciclos				varchar(500),
				Contracecao			varchar(500),	
				Gravidezes			int,
				Partos				int,
				HistoriaObstetrica	varchar(1000),
				DUM					date,
				DPP					date,
				Menopausa			int,
	
			);
		end
		insert into ProjetoConsultorio.DeletedHistorialClinico select * from deleted;
		delete from ProjetoConsultorio.HistorialClinico where PacNIF=(select PacNIF from deleted);
	end
	go
-- Medico --

	-- Indexes
	create index medName on ProjetoConsultorio.Medico(Nome);

	-- Search
	go	-- medico nome
	create proc searchMedByName @user varchar(50)
	as
		select * from ProjetoConsultorio.Medico where Nome like '%'+@user+'%';
	go

	go	-- medico nif
	create proc searchMedByNIF @nif int
	as
		select * from ProjetoConsultorio.Medico where MedNIF=@nif;
	go

	go	-- medico especialidade
	create proc searchMedByEspec @spec varchar(50)
	as
		select * from ProjetoConsultorio.Medico where Especialidade like '%'+@spec+'%';
	go

	go	-- medico todos
	create proc searchMed
	as
		select * from ProjetoConsultorio.Medico
	go


	-- Delete
	go
	create trigger deleteMed on ProjetoConsultorio.Medico
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedMedico'))) begin
			create table ProjetoConsultorio.DeletedMedico (
				ID				int identity(1,1) not null primary key,
				MedNIF				int	not null,
				Nome				varchar(50),
				Mail				varchar(50),
				Especialidade		varchar(50),
				HorasSemanais		int,
				DataInicio			date,
				Salario				real,

			);
		end
		insert into ProjetoConsultorio.DeletedMedico select * from deleted;
		delete from ProjetoConsultorio.Medico where MedNIF=(select MedNIF from deleted);
	end
	go

	go
	create proc deleteMedProcedure @user int
	as
	begin
		begin try
			begin tran
				delete from ProjetoConsultorio.Dirige where Medico=@user;	-- delete dirige
				delete from ProjetoConsultorio.Medico where MedNIF=@user;	-- delete medico
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran 
		end catch
	end
	go


-- Consulta --

	-- Search
	go	-- consulta nif
	create function searchConByNIFInternal(@nif int) returns @consultas table(idconsulta int, dataconsulta date, tipoconsulta varchar(30))
	as
	begin
		declare @tipocon as varchar(30);

		select @tipocon = 'ConsultaGinecologia';
		insert into @consultas select distinct I.ID, I.DataC, @tipocon from ProjetoConsultorio.Consulta as I join ProjetoConsultorio.ConsultaGinecologia as R on I.ID=R.ID where NIFPaciente=@nif;
		
		select @tipocon = 'ConsultaObstetricia';
		insert into @consultas select distinct I.ID, I.DataC, @tipocon from ProjetoConsultorio.Consulta as I join ProjetoConsultorio.ConsultaObstetricia as R on I.ID=R.ID where GravidezPac=@nif;
		
		return;
	end
	go
	go 
	create proc searchConByNIF @nif int
	as
		select * from searchConByNIFInternal(@nif)
		order by idconsulta;
	go

	go	-- data consulta id
	create function searchConDateByID(@id int) returns date
	as
	begin
		declare @dataconsulta as date;
		select @dataconsulta = (select DataC from ProjetoConsultorio.Consulta where ID=@id);
		return @dataconsulta;
	end
	go

	go	-- consulta ginecologia id
	create function searchConGinByID(@id int) returns @retorno table( ID	int, Descricao	varchar(1000), DataC	date, Hora	real )
	as
	begin
		insert into @retorno select ConsultaGinecologia.ID, Descricao, DataC, Hora from ProjetoConsultorio.ConsultaGinecologia, ProjetoConsultorio.Consulta where ConsultaGinecologia.ID=@id and Consulta.ID=@id;
		return
	end
	go

	go	-- consulta obstetricia id
	create function searchConObsByID(@id int) returns @retorno table( ID		int,			GravidezPac		int,		  GravidezNum		int,		  Tipo		varchar(30),
																	  AFU		int,			MAF				bit,		  Foco				bit,		  Peso		real,
																	  TA		varchar(20),	ExGin			varchar(500), Queixas			varchar(500), Medicacao	varchar(500),
																	  Obs		varchar(1000),	PuerAmamentacao	varchar(100), PuerMenstruacao	varchar(100), PuerObservacoes varchar(500), 
																	  DataC		date,			Hora			real )
	as
	begin
		insert into @retorno select ConsultaObstetricia.ID, GravidezPac,GravidezNum,Tipo ,AFU ,MAF	,Foco ,Peso ,TA,ExGin,Queixas,Medicacao,Obs,PuerAmamentacao,PuerMenstruacao,PuerObservacoes, DataC, Hora from ProjetoConsultorio.ConsultaObstetricia, ProjetoConsultorio.Consulta where ConsultaObstetricia.ID=@id and Consulta.ID=@id;
		return
	end
	go

	-- Get
	go	-- get tipo da consulta (0 se gin, 1 se obs)
	create function tipoConsulta (@consulta int) returns int
	as
	begin
		declare @tipo int
		if(exists(select * from ProjetoConsultorio.ConsultaGinecologia where ID=@consulta))
			select @tipo=0
		else
			select @tipo=1
		
		return @tipo

	end
	go

	go	-- get id max das consultas
	create function maxConsulta() returns int
	as
	begin
		return(select max(ID) from ProjetoConsultorio.Consulta);
	end
	go

	-- Update
	go
	create proc changeDataConsulta @id int, @novaData date
	as
	begin
		declare @antigaData date;
		select @antigaData = (select DataC from ProjetoConsultorio.Consulta where ID=@id);
		if((select @novaData)=(select @antigaData)) begin
			return;
		end
		update ProjetoConsultorio.Consulta set DataC=(select @novaData) where ID=@id;					-- update data consulta
		update ProjetoConsultorio.DescricaoAnalise set DataDA=(select @novaData) where Consulta=@id;	-- update data analises entregues na consulta
	end
	go

	-- Delete
	
	go -- consulta
	create trigger deleteCon on ProjetoConsultorio.Consulta
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedConsulta'))) begin
			create table ProjetoConsultorio.DeletedConsulta(
				ID				int identity(1,1),
				IDC				int,
				DataC			date,
				Hora			real,
				NIFPaciente		int,

				primary key(ID),
			);
		end
		insert into ProjetoConsultorio.DeletedConsulta select * from deleted;
		delete from ProjetoConsultorio.Consulta where ID=(select ID from deleted);
	end
	go


	go -- consulta obstetricia
	create trigger deleteConObs on ProjetoConsultorio.ConsultaObstetricia
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedConsultaObstetricia'))) begin
			create table ProjetoConsultorio.DeletedConsultaObstetricia (
				ID			    int identity(1,1),
				IDCO			int not null,
				GravidezPac		int,
				GravidezNum		int,
				Tipo			varchar(20),
				AFU				int,
				MAF				bit,
				Foco			bit,
				Peso			real,
				TA				varchar(20),
				ExGin			varchar(500),
				Queixas			varchar(500),
				Medicacao		varchar(500),
				Obs				varchar(1000),
				PuerAmamentacao varchar(100),
				PuerMenstruacao varchar(100),
				PuerObservacoes varchar(500),

				primary key(ID),
			);
		end



		insert into ProjetoConsultorio.DeletedConsultaObstetricia select * from deleted;
		delete from ProjetoConsultorio.ConsultaObstetricia where ID=(select ID from deleted);
	end
	go

	go -- consulta ginecologia
	create trigger deleteConGin on ProjetoConsultorio.ConsultaGinecologia
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedConsultaGinecologia'))) begin
			create table ProjetoConsultorio.DeletedConsultaGinecologia(
				ID				int identity(1,1),
				IDCG			int not null,
				Descricao		varchar(1000),

				primary key(ID),
			);
		end
		insert into ProjetoConsultorio.DeletedConsultaGinecologia select * from deleted;
		delete from ProjetoConsultorio.ConsultaGinecologia where ID=(select ID from deleted);
	end
	go
	
	go -- delete consulta
	create proc deleteConProcedure @tipoconsulta int, @id int
	as
	begin
		begin try
			begin tran 
				exec deleteAnaliseProcedure @id;												-- analise

				delete from ProjetoConsultorio.Dirige where Consulta=@id;						-- dirige

				if(@tipoconsulta = 0) begin
					delete from ProjetoConsultorio.ConsultaGinecologia where ID=@id;			-- consulta ginecologia
				end else begin
					delete from ProjetoConsultorio.ConsultaObstetricia where ID=@id;			-- consulta obstetricia
				end
				delete from ProjetoConsultorio.Consulta where ID=@id;							-- consulta
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran
		end catch
	end
	go

-- Dirige --
	-- Search
	go -- medico que dirige a consulta id
	create proc searchMedDirige @id int
	as
	begin
		select Medico from ProjetoConsultorio.Dirige where Consulta=@id;
	end
	go

	-- Delete
	go -- dirige
	create trigger deleteDir on ProjetoConsultorio.Dirige
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedDirige'))) begin
			create table ProjetoConsultorio.DeletedDirige(
				Medico			int		not null,
				Consulta		int		not null,

				primary key(Medico, Consulta),
			);
		end
		insert into ProjetoConsultorio.DeletedDirige select * from deleted;
		delete from ProjetoConsultorio.Dirige where Medico=(select Medico from deleted);
	end
	go

-- Gravidez --

	-- Search
	go -- gravidez nif
	create proc searchGravByNIF @nif int
	as
		select * from ProjetoConsultorio.Gravidez where NIFPac=@nif;
	go

	-- Get
	go -- gravidez exists?
	create function gravidezPacienteExists(@nif int, @grav int) returns int
	as
	begin
		declare @ret int
		if (exists(select * from ProjetoConsultorio.Gravidez where NIFPac=@nif and NumeroGravidez=@grav))
			select @ret=1;
		else
			select @ret=0;
		return @ret
	end
	go

	go -- numero de consultas da gravidez
	create function numConsultasGravidez(@nif int, @grav int) returns int
	as
	begin
		return (select NumeroConsultas from ProjetoConsultorio.Gravidez where NIFPac=@nif and NumeroGravidez=@grav)
	end
	go

	-- Delete
	go -- gravidez
	create trigger deleteGrav on ProjetoConsultorio.Gravidez
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedGravidez'))) begin
			create table ProjetoConsultorio.DeletedGravidez(
				NIFPac				int		not null,
				NumeroGravidez		int		not null,
				NumeroConsultas		int		not null,

				primary key(NIFPac, NumeroGravidez),
			);
		end
		insert into ProjetoConsultorio.DeletedGravidez select * from deleted;
		delete from ProjetoConsultorio.Gravidez where NIFPac=(select NIFPac from deleted) and NumeroGravidez=(select NumeroGravidez from deleted);
	end
	go

	go -- delete gravidez
	create proc deleteGravProcedure @nif int, @numgrav int
	as
	begin
		begin try
			begin tran
				declare @numGravAtual as int; select @numGravAtual = (select Gravidezes from ProjetoConsultorio.HistorialClinico where PacNIF=@nif);
				update ProjetoConsultorio.HistorialClinico set Gravidezes=((select @numGravAtual)-1) where PacNIF=@nif;		-- update gravidezes historial clinico

		
				delete from ProjetoConsultorio.Bebe where NIFPac=@nif and Numgravidez=@numgrav;								-- delete bebes

				declare @idconsulta as int;
				declare c cursor fast_forward
				for select ID from ProjetoConsultorio.Consulta where NIFPaciente=@nif;
				open c; fetch c into @idconsulta;
				while @@FETCH_STATUS = 0 begin
					exec deleteConProcedure 1, @idconsulta ;																-- delete consultas
					fetch c into @idconsulta;
				end
				close c; deallocate c;
		
		
				delete from ProjetoConsultorio.Gravidez where NIFPac=@nif and Numerogravidez=@numgrav;						-- delete gravidez
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran
		end catch
	end
	go
	
-- Bebe --

	-- Search
	go	-- bebe nif
	create proc searchBebeByNIF @nif int
	as
		select Bebe, Parto, SexoBebe, PesoBebe, APGAR, Indicacao from ProjetoConsultorio.Paciente join ProjetoConsultorio.Bebe on PacNIF=NIFPac  where NIFPac=@nif;
	go

	-- Get
	go	-- bebe nif numgravidez
	create proc getBebeInfoByNIFnGrav @nifmae int, @numgrav int
	as
		select * from ProjetoConsultorio.Bebe where NIFPac=@nifmae and NumGravidez=@numgrav;
	go

	go	-- num bebes
	create function getNumBebesByNIFnGrav(@nifmae int, @numgrav int) returns int
	as
	begin
		declare @numbebes int;
		select @numbebes = count(*) from ProjetoConsultorio.Bebe where NIFPac=@nifmae and NumGravidez=@numgrav;
		return @numbebes;
	end
	go


	-- Delete
	go -- bebe
	create trigger deleteBebe on ProjetoConsultorio.Bebe
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedBebe'))) begin
			create table ProjetoConsultorio.DeletedBebe(
				NIFPac			int,
				NumGravidez		int,
				Bebe			int,
				Parto			varchar(20),
				Indicacao 		varchar(500),
				SexoBebe 		varchar(15),
				PesoBebe 		real,
				APGAR 			varchar(20),

				primary key(NIFPac, NumGravidez),
			);
		end
		insert into ProjetoConsultorio.DeletedBebe select * from deleted;
		delete from ProjetoConsultorio.Bebe where NIFPac=(select NIFPac from deleted) and NumGravidez=(select NumGravidez from deleted);
	end
	go



-- Requisicao --

	-- Search
	go -- requisicao analise nif
	create proc searchReqAnaliseByNIF @nif int
	as
		select * from ProjetoConsultorio.RequisicaoAnalise where NIFPaciente=@nif;
	go

	go	-- get id max das requisicoes analises
	create function maxReqAnalise() returns int
	as
	begin
		return (select max(IDRA) from ProjetoConsultorio.RequisicaoAnalise);
	end
	go

	--delete 
	go
	create trigger deleteReq on ProjetoConsultorio.RequisicaoAnalise
	instead of delete 
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedRequisicaoAnalise'))) begin
			create table ProjetoConsultorio.DeletedRequisicaoAnalise(
				ID					int identity(1,1),
				IDRA				int not null,
				DataR				date,
				NIFPaciente			int,
				NIFMedico			int,
				Tipo 		varchar(30),

				primary key(ID),
			);
		end

		update ProjetoConsultorio.DescricaoAnalise set Requisicao=null where Requisicao=(select IDRA from deleted);

		insert into DeletedRequisicaoAnalise select * from deleted;
		delete from RequisicaoAnalise where IDRA=(select IDRA from deleted);
	end
	go


	go
	create proc deleteAllReqFromUser @user int
	as
	begin
		begin try
			begin tran
				delete from ProjetoConsultorio.RequisicaoAnalise where NIFPaciente=@user;
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran
		end catch
	end
	go

-- Analise --

	-- Search
	go -- analise nif
	create proc searchAnaliseByNIF @nif int
	as
		select IDDA, Requisicao, DataDA, Descricao from ProjetoConsultorio.Paciente join ProjetoConsultorio.Consulta on PacNIF=NIFPaciente join ProjetoConsultorio.DescricaoAnalise on ID=Consulta where PacNIF=@nif;
	go

	go -- urinas id
	create proc searchUrinaByID @id int
	as
		select * from ProjetoConsultorio.Urina  where IDDA=@id;
	go

	go -- rx id
	create proc searchRXByID @id int
	as
		select * from ProjetoConsultorio.RX  where IDDA=@id;
	go

	go -- sangue id
	create proc searchSangueByID @id int
	as
		select * from ProjetoConsultorio.Sangue  where IDDA=@id;
	go

	go -- cmcolo id
	create proc searchCMColoByID @id int
	as
		select * from ProjetoConsultorio.CMColo  where IDDA=@id;
	go
	
	go -- exsudado id
	create proc searchExsudadoByID @id int
	as
		select * from ProjetoConsultorio.Exsudado  where IDDA=@id;
	go
	
	go -- mama id
	create proc searchMamaByID @id int
	as
		select * from ProjetoConsultorio.Mama  where IDDA=@id;
	go

	-- Get
	go -- get info analise
	create proc getAnaliseInfoByID @id int
	as
		select DataDA, Descricao from ProjetoConsultorio.DescricaoAnalise where IDDA=@id;
	go

	go -- get tipo da analise 
	create function tipoAnalise(@id int) returns varchar(30)
	as
	begin
		declare @retorno varchar(30)

		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.Sangue on DescricaoAnalise.IDDA=Sangue.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='Sangue';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.Urina on DescricaoAnalise.IDDA=Urina.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='Urina';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.RX on DescricaoAnalise.IDDA=RX.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='RX';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.Mama on DescricaoAnalise.IDDA=Mama.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='Mama';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.HSG on DescricaoAnalise.IDDA=HSG.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='HSG';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.Exsudado on DescricaoAnalise.IDDA=Exsudado.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='Exsudado';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.Espermograma on DescricaoAnalise.IDDA=Espermograma.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='Espermograma';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.EcografiaGinecologica on DescricaoAnalise.IDDA=EcografiaGinecologica.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='EcografiaGinecologia';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.EcografiaObstetrica on DescricaoAnalise.IDDA=EcografiaObstetrica.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='EcografiaObstetrica';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.ECG on DescricaoAnalise.IDDA=ECG.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='ECG';
		if(exists(select * from ProjetoConsultorio.DescricaoAnalise join ProjetoConsultorio.CMColo on DescricaoAnalise.IDDA=CMColo.IDDA where DescricaoAnalise.IDDA=@id))
			select @retorno='CMColo';

		return @retorno
	end
	go

	go	-- get id max das analises
	create function maxAnalise() returns int
	as
	begin
		return(select max(IDDA) from ProjetoConsultorio.DescricaoAnalise);
	end
	go

	go	-- get requisicao (da descricao) id
	create function getReqAnalise(@id int) returns int
	as
	begin
		return (select Requisicao from ProjetoConsultorio.DescricaoAnalise where IDDA=@id);
	end
	go

	-- Delete 
	go
	create trigger deleteDescricao on ProjetoConsultorio.DescricaoAnalise
	instead of delete 
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedDescricaoAnalise'))) begin
			create table ProjetoConsultorio.DeletedDescricaoAnalise(
				ID				int identity(1,1),
				IDDA			int not null,
				Consulta		int,
				Requisicao		int,
				DataDA			date,
				Descricao		varchar(500),

				primary key(ID),
			);
		end

		insert into DeletedDescricaoAnalise select * from deleted;
		delete from DescricaoAnalise where IDDA=(select IDDA from deleted);
	end
	go

	go -- sangue
	create trigger deleteSangue on ProjetoConsultorio.Sangue
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedSangue'))) begin
			create table ProjetoConsultorio.DeletedSangue(
				ID				int identity(1,1)  primary key,
				IDDA						int 			not null,
				Rastreio1Trim				varchar(50), 
				Rastreio2Trim				varchar(50),

				GrupoSanguineo				varchar(10),
				CoombsIndireto				bit,
				HemogramaHemoglobina		real,
				Plaquetas					int,
				TP							varchar(20),
				TTP							varchar(20),
				VS							real,

				Glicose						real,
				PTGO						varchar(20),
				Ureia						real,
				Creatinina					real,
				AcidoUrico					real,
				FosfataseAlcalina			real,
				BilirubinasDireta			real,
				BilirubinasIndireta			real,
				TGO							real,
				TGP							real,
	
				ColestTotal					real,
				Triglicerideos				real,
				HDL							real,
				LDL							real,

				VDRL						bit,
				SerologiaCMV_IgG			varchar(20),
				SerologiaCMV_IgM			varchar(20),
				SerologiaRubeola_IgG		varchar(20),
				SerologiaRubeola_IgM		varchar(20),
				SerologiaToxoplasmose_IgG	varchar(20),
				SerologiaToxoplasmose_IgM	varchar(20),
				AgHBs						bit,
				HCV							bit,
				HIV							bit,
	
				TSH							real,
				T3							real,
				T4							real,
				FSH							real,
				LH							real,
				Estradiol					real,
				Progesterona				real,
				Prolactina					real,
				DHEA						real,
				Testosterona				real,
	
				CA125						real,
				CA15_3						real,
				CA19_9						real,
				CEA							real,

				AntiCoagulanteLupico		bit,
				Antifosfolipidico			bit,
				Antitrombina				bit,
				AntiDNA						bit,
				AntiCardiolipina			bit,
				AntiGliadina				bit,
				AntiNucleares				bit,
				AntiEndomisio				bit,
				AntiReticulina				bit,
				AntiTransglutaminase		bit,
				ProteinaC					bit,
				ProteinaS					bit,
				Imunoglobulinas				bit,
				Glicoproteina				bit,

				Outros						varchar(200),
			);
		end

		insert into ProjetoConsultorio.DeletedSangue select * from deleted;
		delete from ProjetoConsultorio.Sangue where IDDA=(select IDDA from deleted);

	end
	go

	go -- urina
	create trigger deleteUrina on ProjetoConsultorio.Urina
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedUrina'))) begin
			create table ProjetoConsultorio.DeletedUrina(
				ID				int identity(1,1)  primary key,
				IDDA			int 			not null,
				SumariaUrinas	varchar(20),
				Urocultura		bit,
				ValorUroc		varchar(20)     default null,
				TIG				bit,
			);
		end

		insert into ProjetoConsultorio.DeletedUrina select * from deleted;
		delete from ProjetoConsultorio.Urina where IDDA=(select IDDA from deleted);

	end
	go

	go -- cmcolo
	create trigger deleteCMColo on ProjetoConsultorio.CMColo
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedCMColo'))) begin
			create table ProjetoConsultorio.DeletedCMColo(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null  ,
				Convencional	varchar(100),
				MeioLiquido		varchar(100),
			);
		end

		insert into ProjetoConsultorio.DeletedCMColo select * from deleted;
		delete from ProjetoConsultorio.CMColo where IDDA=(select IDDA from deleted);

	end
	go

	go -- rx
	create trigger deleteRX on ProjetoConsultorio.RX
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedRX'))) begin
			create table ProjetoConsultorio.DeletedRX(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
				Tipo		varchar(100),
			);
		end

		insert into ProjetoConsultorio.DeletedRX select * from deleted;
		delete from ProjetoConsultorio.RX where IDDA=(select IDDA from deleted);

	end
	go

	go -- hsg
	create trigger deleteHSG on ProjetoConsultorio.HSG
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedHSG'))) begin
			create table ProjetoConsultorio.DeletedHSG(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null ,
			);
		end

		insert into ProjetoConsultorio.DeletedHSG select * from deleted;
		delete from ProjetoConsultorio.HSG where IDDA=(select IDDA from deleted);

	end
	go

	go -- EcografiaGinecologica
	create trigger deleteEcografiaGinecologica on ProjetoConsultorio.EcografiaGinecologica
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedEcografiaGinecologica'))) begin
			create table ProjetoConsultorio.DeletedEcografiaGinecologica(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
			);
		end

		insert into ProjetoConsultorio.DeletedEcografiaGinecologica select * from deleted;
		delete from ProjetoConsultorio.EcografiaGinecologica where IDDA=(select IDDA from deleted);

	end
	go

	go -- EcografiaObstetrica
	create trigger deleteEcografiaObstetrica on ProjetoConsultorio.EcografiaObstetrica
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedEcografiaObstetrica'))) begin
			create table ProjetoConsultorio.DeletedEcografiaObstetrica(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
				Semana		int,
				Dias		int,
			);
		end

		insert into ProjetoConsultorio.DeletedEcografiaObstetrica select * from deleted;
		delete from ProjetoConsultorio.EcografiaObstetrica where IDDA=(select IDDA from deleted);

	end
	go

	go -- Espermograma
	create trigger deleteEspermograma on ProjetoConsultorio.Espermograma
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedEspermograma'))) begin
			create table ProjetoConsultorio.DeletedEspermograma(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
			);
		end

		insert into ProjetoConsultorio.DeletedEspermograma select * from deleted;
		delete from ProjetoConsultorio.Espermograma where IDDA=(select IDDA from deleted);

	end
	go
	 
	go -- ECG
	create trigger deleteECG on ProjetoConsultorio.ECG
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedECG'))) begin
			create table ProjetoConsultorio.DeletedECG(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null ,
			);
		end

		insert into ProjetoConsultorio.DeletedECG select * from deleted;
		delete from ProjetoConsultorio.ECG where IDDA=(select IDDA from deleted);

	end
	go

	go -- mama
	create trigger deleteMama on ProjetoConsultorio.Mama
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedMama'))) begin
			create table ProjetoConsultorio.DeletedMama(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
				Mamografia			varchar(100),
				EcografiaMamaria	varchar(100),
			);
		end

		insert into ProjetoConsultorio.DeletedMama select * from deleted;
		delete from ProjetoConsultorio.Mama where IDDA=(select IDDA from deleted);

	end
	go

	go -- Exsudado
	create trigger deleteExsudado on ProjetoConsultorio.Exsudado
	instead of delete
	as
	begin
		if (not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_SCHEMA='ProjetoConsultorio' and TABLE_NAME = 'DeletedExsudado'))) begin
			create table ProjetoConsultorio.DeletedExsudado(
				ID				int identity(1,1)  primary key,
				IDDA			int			not null,
				VaginalBacteriologico	varchar(50),
				VaginalMicologico		varchar(50),
				VaginalParasitologico	varchar(50),
				VaginoRetalSGB			bit,
			);
		end

		insert into ProjetoConsultorio.DeletedExsudado select * from deleted;
		delete from ProjetoConsultorio.Exsudado where IDDA=(select IDDA from deleted);

	end
	go

	go -- Descricao Analise
	create proc deleteAnaliseProcedure @id int
	as
	begin
		begin try
			begin tran
				declare @tipo varchar(30);
				set @tipo = (select dbo.tipoAnalise(@id));
				if(@tipo='EcografiaGinecologica')
					delete from ProjetoConsultorio.EcografiaGinecologica where IDDA=@id;
				if(@tipo='EcografiaObstetrica')
					delete from ProjetoConsultorio.EcografiaObstetrica where IDDA=@id;
				if(@tipo='ECG')
					delete from ProjetoConsultorio.ECG where IDDA=@id;
				if(@tipo='HSG')
					delete from ProjetoConsultorio.HSG where IDDA=@id;
				if(@tipo='Espermograma')
					delete from ProjetoConsultorio.Espermograma where IDDA=@id;
				if(@tipo='CMColo')
					delete from ProjetoConsultorio.CMColo where IDDA=@id;
				if(@tipo='RX')
					delete from ProjetoConsultorio.RX where IDDA=@id;
				if(@tipo='Mama')
					delete from ProjetoConsultorio.Mama where IDDA=@id;
				if(@tipo='Exsudado')
					delete from ProjetoConsultorio.Exsudado where IDDA=@id;
				if(@tipo='Urina')
					delete from ProjetoConsultorio.Urina where IDDA=@id;
				if(@tipo='Sangue')
					delete from ProjetoConsultorio.Sangue where IDDA=@id;

				delete from ProjetoConsultorio.DescricaoAnalise where IDDA=@id;
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran
		end catch
	end
	go

	go
	create proc deleteAllDescrFromUser @user int
	as
	begin
		begin try
			begin tran
				declare @idDesc as int;
				declare c cursor fast_forward
				for select IDDA from ProjetoConsultorio.Consulta join ProjetoConsultorio.DescricaoAnalise on ID=Consulta where NIFPaciente=@user;
				open c; fetch c into @idDesc;
				while @@FETCH_STATUS = 0 begin
					exec deleteAnaliseProcedure @idDesc ;																-- delete consultas
					fetch c into @idDesc;
				end
				close c; deallocate c;
			commit tran
		end try
		begin catch
			if(@@TRANCOUNT>0)
				rollback tran
		end catch

	end
	go



	go
	create function IMC(@user int) returns int
	as
	begin
		declare @peso real;
		declare @altura real;
		select @peso=Peso, @altura=Altura from ProjetoConsultorio.HistorialClinico where PacNIF=@user;

		return (@peso/(select power(@altura, 2)));

	end
	go

	
	go
	create function IG(@user int) returns varchar(50)
	as
	begin
		declare @weeks int;
		declare @days int;
		declare @dum date;
		declare @currDate date;
		declare @ret varchar(50);

		set @currDate = SYSDATETIME();
		set @dum = (select DUM from ProjetoConsultorio.HistorialClinico where PacNIF=@user)

		set @weeks= DATEDIFF(WEEK, @dum, @currDate);
		set @days= DATEDIFF(DAY, @dum, @currDate);
		
		return cast(@weeks as varchar) + ' Semanas, ' + cast(@days%7 as varchar) + ' Dias'
	end
	go


	go
	create function ecoObsPac(@user int) returns @ret table(IDDA int, DataE date)
	as
	begin
		insert into @ret select DescricaoAnalise.IDDA, DescricaoAnalise.DataDA from ProjetoConsultorio.Consulta join ProjetoConsultorio.DescricaoAnalise on ID=Consulta join ProjetoConsultorio.EcografiaObstetrica on DescricaoAnalise.IDDA=EcografiaObstetrica.IDDA where NIFPaciente=@user
		return
	end
	go


	go
	create function IGEco(@id int) returns varchar(50)
	as
	begin
		declare @weeks int;
		declare @days int;
		declare @dum date;
		declare @currDate date;
		declare @ret varchar(50);

		set @currDate = SYSDATETIME();
		set @dum = (select DataDA from ProjetoConsultorio.DescricaoAnalise where IDDA=@id)

		set @weeks= DATEDIFF(WEEK, @dum, @currDate);
		set @days= DATEDIFF(DAY, @dum, @currDate);
		
		return cast(@weeks as varchar) + ' Semanas, ' + cast(@days as varchar) + ' Dias'
	end
	go


	go
	create function ecoObsDate(@id int) returns date
	as
	begin
		return(select DataDA from ProjetoConsultorio.DescricaoAnalise where IDDA=@id);
	end
	go

	
	go
	create function gotHC(@user int) returns int
	as
	begin
		declare @ret int;
		if ( exists (select * from ProjetoConsultorio.HistorialClinico where PacNIF=@user) )
			set @ret=1;
		else
			set @ret=0;
		return @ret;
	end
	go
use BD_Project;

-- Paciente
-- exec searchByName 'Paciente0';
-- exec searchBySubsist 'ADSE';
-- exec searchByNIF 10000001;
-- exec searchBasicInfoByNIF 10000001;
-- exec searchAllPac;
exec deletePacProcedure 10000001; -- falta o deleteAllDescrFromUser

-- Historial Clinico
-- exec searchPacHC 10000001;
-- delete from ProjetoConsultorio.HistorialClinico where PacNIF=10000001;
-- exec searchPacHC 10000001; -- ja nao existe
-- exec searchPacHC 10000002;
-- delete from ProjetoConsultorio.HistorialClinico where PacNIF=10000002;
-- select * from ProjetoConsultorio.DeletedHistorialClinico; -- contem os dois eliminados

-- Medico
-- exec searchMedByName 'Joao';
-- exec searchMedByNIF 11111111;
-- exec searchMedByEspec 'Ginecologia';
-- exec searchMed;
exec deleteMedProcedure 11111111;

-- Consulta
-- exec searchConByNIF 10000003;
-- declare @date date; select @date = dbo.searchConDateByID(1); select @date;
-- select * from dbo.searchConGinByID(1);
-- select * from dbo.searchConObsByID(183);
-- exec changeDataConsulta 2, '2010-06-22';
-- declare @tipo int; select @tipo = dbo.tipoConsulta(1); select @tipo;
-- declare @tipo int; select @tipo = dbo.tipoConsulta(183); select @tipo;
-- declare @maxid int; select @maxid = dbo.maxConsulta(); select @maxid;

-- Dirige
-- exec searchMedDirige 1;
-- delete from ProjetoConsultorio.Dirige where consulta=1;
-- exec searchMedDirige 1;
-- exec searchMedDirige 2;
-- select * from ProjetoConsultorio.DeletedDirige; -- contem o unico eliminado

-- Gravidez
-- exec searchGravByNIF 10000115;
-- declare @exists bit; select @exists = dbo.gravidezPacienteExists(10000115,4); select @exists; -- true
-- declare @exists bit; select @exists = dbo.gravidezPacienteExists(10000115,1); select @exists; -- false
-- declare @numconsultas int; select @numconsultas = dbo.numConsultasGravidez(10000115,4); select @numconsultas;
-- select NumeroGravidez from ProjetoConsultorio.Gravidez where NIFPac=10000115;
-- select Gravidezes from ProjetoConsultorio.HistorialClinico where PacNIF=10000115;
-- select GravidezNum from ProjetoConsultorio.ConsultaObstetricia join ProjetoConsultorio.Consulta on ConsultaObstetricia.ID=Consulta.ID where NIFPaciente=10000115;
-- exec deleteGravProcedure 10000115, 4;

-- Bebe

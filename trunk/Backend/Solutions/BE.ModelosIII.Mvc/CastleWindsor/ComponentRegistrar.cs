using System;
using System.IO;
using AutoMapper;
using BE.ModelosIII.Infrastructure.ApplicationServices;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentValidation;
using SharpArch.Domain.Commands;
using SharpArch.Domain.Events;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;
using SharpArch.Web.Mvc.Castle;

namespace BE.ModelosIII.Mvc.CastleWindsor
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddApplicationServicesTo(container);
            AddAutoMapperTo(container);
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddQueryObjectsTo(container);
            AddTasksTo(container);
            AddHandlersTo(container);
            AddValidatorsTo(container);
        }

        private static void AddApplicationServicesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For<IFileSystem>()
                    .ImplementedBy<FileSystem>());
            container.Register(
             Component.For<IPdfManager>()
                 .ImplementedBy<PdfManager>()
                    .DependsOn(new { workPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/img") }));
            container.Register(
                Component.For<IValidatorFactory>()
                         .ImplementedBy<WindsorValidatorFactory>());
            container.Register(
                Component.For<IEncryptionService>()
                         .ImplementedBy<EncryptionService>());
            container.Register(
                Component.For<IEmailService>()
                    .ImplementedBy<EmailService>());
        }

        public static void AddAutoMapperTo(IWindsorContainer container)
        {
            container.Register(
                Component.For<IMappingEngine>()
                         .Instance(Mapper.Engine)
                );
        }

        private static void AddTasksTo(IWindsorContainer container)
        {
            container.Register(
                Classes
                    .FromAssemblyNamed("BE.ModelosIII.Tasks")
                    .Pick().If(t => t.Name.EndsWith("Tasks"))
                    .WithService.FirstNonGenericCoreInterface("BE.ModelosIII.Domain")
                );
        }
    
        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Classes
                    .FromAssemblyNamed("BE.ModelosIII.Infrastructure")
                    .BasedOn(typeof(IRepositoryWithTypedId<,>))
                    .WithService.FirstNonGenericCoreInterface("BE.ModelosIII.Domain"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType")
                    .Forward(typeof(IRepository<>)));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId")
                    .Forward(typeof(IRepositoryWithTypedId<,>)));

            container.Register(
                    Component.For(typeof(ISessionFactoryKeyProvider))
                        .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                        .Named("sessionFactoryKeyProvider"));

            container.Register(
                    Component.For(typeof(ICommandProcessor))
                        .ImplementedBy(typeof(CommandProcessor))
                        .Named("commandProcessor"));
        }

        private static void AddQueryObjectsTo(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Mvc")
                    .BasedOn<NHibernateQuery>()
                    .WithService.DefaultInterfaces());

            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Infrastructure")
                    .BasedOn(typeof(NHibernateQuery))
                    .WithService.DefaultInterfaces());
        }

        private static void AddHandlersTo(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Tasks")
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithService.FromInterface());  
            
            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Tasks")
                    .BasedOn(typeof(ICommandHandler<,>))
                    .WithService.FromInterface());

            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Tasks")
                    .BasedOn(typeof(IHandles<>))
                    .WithService.FirstInterface());
        }

        private static void AddValidatorsTo(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("BE.ModelosIII.Tasks")
                .BasedOn(typeof(IValidator<>))
                .WithService.Base());
        }
    }
}
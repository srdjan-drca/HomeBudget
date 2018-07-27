using System;

namespace HomeBudget.Tools.SimpleIoc {

   public interface IContainer {

      void Register<TTypeToResolve, TConcrete>();

      void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle);

      TTypeToResolve Resolve<TTypeToResolve>();

      object Resolve(Type typeToResolve);
   }
}
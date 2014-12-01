using System.Collections.Generic;
using BE.ModelosIII.Domain.Contracts;
using SharpArch.Domain.DomainModel;

namespace BE.ModelosIII.Domain
{
    public class Run : Entity
    {
        public virtual Scenario Scenario { get; set; }
        public virtual IList<Generation> Generations { get; set; }
        public virtual System.DateTime RunOn { get; set; }
        public virtual float CrossoverProbability { get; set; }
        public virtual float MutationProbability { get; set; }
        public virtual int PopulationSize { get; set; }
        public virtual int StopDepth { get; set; }
        public virtual bool Report { get; set; }
        public virtual Enums.StopCriterion StopCriterion { get; set; }
    }
}
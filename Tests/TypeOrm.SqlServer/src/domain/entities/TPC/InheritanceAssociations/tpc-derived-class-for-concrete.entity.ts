import { TpcConcreteBaseClass } from './tpc-concrete-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpc_derived_class_for_concrete')
export class TpcDerivedClassForConcrete extends TpcConcreteBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}
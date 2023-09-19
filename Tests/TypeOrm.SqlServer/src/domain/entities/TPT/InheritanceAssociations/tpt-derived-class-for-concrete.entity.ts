import { TptConcreteBaseClass } from './tpt-concrete-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpt_derived_class_for_concrete')
export class TptDerivedClassForConcrete extends TptConcreteBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}
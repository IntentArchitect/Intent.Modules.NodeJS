import { TphConcreteBaseClass } from './tph-concrete-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tph_derived_class_for_concrete')
export class TphDerivedClassForConcrete extends TphConcreteBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}
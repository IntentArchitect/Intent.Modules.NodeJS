import { TptAbstractBaseClass } from './tpt-abstract-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpt_derived_class_for_abstract')
export class TptDerivedClassForAbstract extends TptAbstractBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}
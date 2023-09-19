import { TphAbstractBaseClass } from './tph-abstract-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tph_derived_class_for_abstract')
export class TphDerivedClassForAbstract extends TphAbstractBaseClass {
  @Column({ length: 250 })
  derivedAttribute: string;

}
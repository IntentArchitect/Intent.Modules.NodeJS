import { TphFkBaseClass } from './tph-fk-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tph_fk_derived_class')
export class TphFkDerivedClass extends TphFkBaseClass {
  @Column()
  derivedField: string;

}